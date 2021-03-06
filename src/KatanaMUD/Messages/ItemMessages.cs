﻿using System;
using KatanaMUD.Models;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace KatanaMUD.Messages
{
    public class InventoryCommand : MessageBase
    {
        public override void Process(Actor actor)
        {
            var response = new InventoryListMessage();
            response.Cash = Game.Data.AllCurrencies.Select(x => new CurrencyDescription(x, Currency.Get(x, actor.Cash))).Where(x => x.Amount > 0).ToArray();
            response.TotalCash = new CurrencyDescription(Game.Data.AllCurrencies.First(x => x.Value == 1), Game.Data.AllCurrencies.Sum(x => Currency.Get(x, actor.Cash) * x.Value));
            response.Items = actor.Items.Select(x => new ItemDescription(x)).ToArray();
            response.Encumbrance = actor.Encumbrance;
            response.MaxEncumbrance = actor.MaxEncumbrance;
            actor.SendMessage(response);
        }
    }

    public class InventoryListMessage : MessageBase
    {
        public CurrencyDescription[] Cash { get; set; }
        public CurrencyDescription TotalCash { get; set; }
        public ItemDescription[] Items { get; set; }
        public long Encumbrance { get; set; }
        public long MaxEncumbrance { get; set; }
        public long Currency { get; set; }
    }

    public class GetItemCommand : MessageBase
    {
        public Guid? ItemId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }

        public override void Process(Actor actor)
        {
            if (Quantity < 0)
            {
                actor.SendMessage(new ActionNotAllowedMessage() { Message = "Cannot get negative items." });
                return;
            }

            var availableCurrencies = Game.Data.AllCurrencies.Where(x => actor.Room.GetTotalCashUserCanSee(x, actor).Total > 0).ToList();

            IEnumerable<IItem> items;

            if (ItemId != null)
            {
                items = actor.Room.Items.Where(x => x.Id == ItemId).Group();
            }
            else
            {
                items = FindItems(availableCurrencies, actor.Room.ItemsUserCanSee(actor).ToList(), ItemName);
            }

            // Handle a cash get.
            if (items.FirstOrDefault() is Currency)
            {
                var currency = items.First() as Currency;
                if (Quantity == 0)
                {
                    // In the event that no number is specified (ie 0), then we assume the user
                    // wants to get all currency. So, we oblige them.
                    Quantity = (int)actor.Room.GetTotalCashUserCanSee(currency, actor).Total;
                }

                var action = actor.CanPickUpCash(currency, Quantity);
                if (action.Allowed)
                {
                    actor.PickUpCash(currency, Quantity);
                    var message = new CashTransferMessage()
                    {
                        Taker = new ActorDescription(actor),
                        Currency = new CurrencyDescription(currency, Quantity)
                    };
                    actor.Room.ActiveActors.Where(x => x != actor).ForEach(x => x.SendMessage(message));
                    actor.SendMessage(message);
                }
                else
                {
                    actor.SendMessage(new ActionNotAllowedMessage() { Message = action.FirstPerson });
                }
                return;
            }

            // Handle a regular get
            if (items.FirstOrDefault() is ItemGroup)
            {
                var group = items.First() as ItemGroup;
                Quantity = Math.Max(Quantity, 1);   // 0 is valid in the event that no number is specified. In that instance, we assume 1 instead.

                List<Item> successes = new List<Item>();
                List<string> failures = new List<string>();

                foreach (var item in group.Items.Take(Quantity))
                {
                    var action = actor.CanPickUpItem(item);

                    if (action.Allowed)
                    {
                        actor.PickupItem(item);
                        successes.Add(item);
                    }
                    else
                    {
                        failures.Add(action.FirstPerson);
                    }
                }

                if (successes.Any())
                {
                    var message = new ItemOwnershipMessage()
                    {
                        Taker = new ActorDescription(actor),
                        Items = successes.Select(x => new ItemDescription(x)).ToArray()
                    };
                    actor.Room.ActiveActors.ForEach(x => x.SendMessage(message));
                }
                if (failures.Any())
                {
                    // send failure messages, but use "distinct" in case someone tries to get 1000 potions, but can only carry 1.
                    // You know someone will do it...
                    failures.Distinct().ForEach(x => actor.SendMessage(new ActionNotAllowedMessage() { Message = x }));
                }
                return;
            }

            actor.SendMessage(new ActionNotAllowedMessage() { Message = "Cannot find item!" });
        }

        /// <summary>
        /// Finds items based on supplied item name. Returns all items that match the given name. Currencies are returned as currnecy objects, 
        /// items are returned in ItemGroup objects.
        /// </summary>
        /// <param name="availableCurrencies"></param>
        /// <param name="availableItems"></param>
        /// <param name="itemName"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public static IEnumerable<IItem> FindItems(IEnumerable<Currency> availableCurrencies, IEnumerable<Item> availableItems, string itemName)
        {
            IEnumerable<IItem> list;
            list = availableCurrencies.Cast<IItem>().Concat(availableItems.Group());
            return list.FindByName(itemName, x => x.Name, true, true);
        }
    }

    public class DropItemCommand : MessageBase
    {
        public Guid[] ItemIds { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public bool Hide { get; set; }

        public override void Process(Actor actor)
        {
            if (Quantity < 0)
            {
                actor.SendMessage(new ActionNotAllowedMessage() { Message = "Cannot drop negative items." });
                return;
            }

            IEnumerable<IItem> items;
            if (ItemIds != null && ItemIds.Any())
            {
                items = actor.Items.Where(x => ItemIds.Contains(x.Id));
            }
            else
            {
                var availableCurrencies = Game.Data.AllCurrencies.Where(x => Currency.Get(x, actor.Cash) > 0).ToList();
                items = GetItemCommand.FindItems(availableCurrencies, actor.Items.ToList(), ItemName);
            }

            if (items.Count() > 1)
            {
                actor.SendMessage(new AmbiguousItemMessage() { Items = items.Select(x => new ItemDescription(x)).ToArray() });
                return;
            }

            if (!items.Any())
            {
                actor.SendMessage(new ActionNotAllowedMessage() { Message = "Cannot find item!" });
                return;
            }

            // Handle a cash drop.
            if (items.First() is Currency)
            {
                var currency = items.First() as Currency;
                if (Quantity == 0)
                {
                    // In the event that no number is specified (ie 0), then we assume the user
                    // wants to drop all currency. So, we oblige them.
                    // TODO: see if this is a correct assumption. Could be dangerous?
                    Quantity = (int)Currency.Get(currency, actor.Cash);
                }

                var action = actor.CanDropCash(currency, Quantity, Hide);
                if (action.Allowed)
                {
                    actor.DropCash(currency, Quantity, Hide);

                    var message = new CashTransferMessage()
                    {
                        Giver = new ActorDescription(actor),
                        Currency = new CurrencyDescription(currency, Quantity),
                        Hide = Hide
                    };

                    if (Hide == false)
                    {
                        // let the actors in the room know, but only if it's not being hidden.
                        actor.Room.ActiveActors.Where(x => x != actor).ForEach(x => x.SendMessage(message));
                    }
                    actor.SendMessage(message);
                }
                else
                {
                    actor.SendMessage(new ActionNotAllowedMessage() { Message = action.FirstPerson });
                }
                return;
            }

            // Handle a regular drop
            Quantity = Math.Max(Quantity, 1);   // 0 is valid in the event that no number is specified. In that instance, we assume 1 instead.

            IEnumerable<Item> finalItems;
            if (items.First() is ItemGroup)
            {
                finalItems = (items.First() as ItemGroup).Items.Take(Quantity);
            }
            else
            {
                finalItems = items.Cast<Item>();
            }

            List<Item> successes = new List<Item>();
            List<string> failures = new List<string>();

            foreach (var item in finalItems)
            {
                var action = actor.CanDropItem(item, Hide);

                if (action.Allowed)
                {
                    actor.DropItem(item, Hide);
                    successes.Add(item);
                }
                else
                {
                    failures.Add(action.FirstPerson);
                }
            }

            if (successes.Any())
            {
                var message = new ItemOwnershipMessage()
                {
                    Giver = new ActorDescription(actor),
                    Items = successes.Select(x => new ItemDescription(x)).ToArray(),
                    Hide = Hide
                };

                if (Hide == false)
                {
                    actor.Room.ActiveActors.ForEach(x => x.SendMessage(message));
                }
                else
                {
                    actor.SendMessage(message);
                }
            }
            if (failures.Any())
            {
                failures.Distinct().ForEach(x => actor.SendMessage(new ActionNotAllowedMessage() { Message = x }));
            }
        }
    }

    public class ItemOwnershipMessage : MessageBase
    {
        public ItemDescription[] Items { get; set; }
        public ActorDescription Giver { get; set; }
        public ActorDescription Taker { get; set; }
        public bool Hide { get; set; }
    }

    public class CashTransferMessage : MessageBase
    {
        public CurrencyDescription Currency { get; set; }
        public ActorDescription Giver { get; set; }
        public ActorDescription Taker { get; set; }
        public bool Hide { get; set; }
    }

    public class GiveCommand : MessageBase
    {
        public Guid[] ItemIds { get; set; }
        public string ItemName { get; set; }
        public Guid? ActorId { get; set; }
        public string ActorName { get; set; }
        public int Quantity { get; set; }

        public override void Process(Actor actor)
        {
            if (Quantity < 0)
            {
                actor.SendMessage(new ActionNotAllowedMessage() { Message = "Cannot give negative items." });
                return;
            }

            IEnumerable<Actor> receivers;
            if (ActorId != null)
            {
                receivers = actor.Room.ActiveActors.Where(x => x.Id == ActorId.Value);
            }
            else
            {
                receivers = actor.Room.ActiveActors.FindActorsByName(ActorName, true).ToList();
            }
            if (!receivers.Any())
            {
                actor.SendMessage(new ActionNotAllowedMessage() { Message = "Cannot find player!" });
                return;
            }
            if (receivers.Count() > 1)
            {
                actor.SendMessage(new AmbiguousActorMessage() { Actors = receivers.Select(x => new ActorDescription(x)).ToArray() });
                return;
            }
            var receiver = receivers.First();

            IEnumerable<IItem> items;
            if (ItemIds != null && ItemIds.Any())
            {
                items = actor.Items.Where(x => ItemIds.Contains(x.Id));
            }
            else
            {
                var availableCurrencies = Game.Data.AllCurrencies.Where(x => Currency.Get(x, actor.Cash) > 0).ToList();
                items = GetItemCommand.FindItems(availableCurrencies, actor.Items.ToList(), ItemName);
            }
            if (items.Count() > 1)
            {
                actor.SendMessage(new AmbiguousItemMessage() { Items = items.Select(x => new ItemDescription(x)).ToArray() });
                return;
            }
            if (!items.Any())
            {
                actor.SendMessage(new ActionNotAllowedMessage() { Message = "Cannot find item!" });
                return;
            }

            // Handle a cash give.
            if (items.First() is Currency)
            {
                var currency = items.First() as Currency;
                if (Quantity == 0)
                {
                    // In the event that no number is specified (ie 0), then we assume the user
                    // wants to drop all currency. So, we oblige them.
                    // TODO: see if this is a correct assumption. Could be dangerous?
                    Quantity = (int)Currency.Get(currency, actor.Cash);
                }

                var action = actor.CanRemoveCash(currency, Quantity);
                if (!action.Allowed)
                {
                    actor.SendMessage(new ActionNotAllowedMessage() { Message = action.FirstPerson });
                    return;
                }
                action = receiver.CanAcceptCash(currency, Quantity);
                if (!action.Allowed)
                {
                    actor.SendMessage(new ActionNotAllowedMessage() { Message = action.ThirdPerson });
                    return;
                }

                var quantity = actor.RemoveCash(currency, Quantity);
                receiver.AcceptCash(currency, quantity);

                var message = new CashTransferMessage()
                {
                    Giver = new ActorDescription(actor),
                    Taker = new ActorDescription(receiver),
                    Currency = new CurrencyDescription(currency, quantity)
                };

                actor.Room.ActiveActors.ForEach(x => x.SendMessage(message));
                return;
            }

            // Handle a regular give
            Quantity = Math.Max(Quantity, 1);   // 0 is valid in the event that no number is specified. In that instance, we assume 1 instead.

            IEnumerable<Item> finalItems;
            if (items.First() is ItemGroup)
            {
                finalItems = (items.First() as ItemGroup).Items.Take(Quantity);
            }
            else
            {
                finalItems = items.Cast<Item>();
            }

            List<Item> successes = new List<Item>();
            List<string> failures = new List<string>();

            foreach (var item in finalItems)
            {
                var give = actor.CanRemoveItem(item);
                var take = receiver.CanAcceptItem(item);

                if (give.Allowed && take.Allowed)
                {
                    actor.RemoveItem(item);
                    receiver.AcceptItem(item);
                    successes.Add(item);
                }

                if (!give.Allowed)
                {
                    failures.Add(give.FirstPerson);
                }
                if (!take.Allowed)
                {
                    failures.Add(take.ThirdPerson);
                }
            }

            if (successes.Any())
            {
                var message = new ItemOwnershipMessage()
                {
                    Giver = new ActorDescription(actor),
                    Taker = new ActorDescription(receiver),
                    Items = successes.Select(x => new ItemDescription(x)).ToArray(),
                };

                actor.Room.ActiveActors.ForEach(x => x.SendMessage(message));
            }
            if (failures.Any())
            {
                failures.Distinct().ForEach(x => actor.SendMessage(new ActionNotAllowedMessage() { Message = x }));
            }
        }
    }
}