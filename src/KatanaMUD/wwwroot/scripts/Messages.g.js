var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var KMud;
(function (KMud) {
    var ActionNotAllowedMessage = (function (_super) {
        __extends(ActionNotAllowedMessage, _super);
        function ActionNotAllowedMessage() {
            _super.call(this, 'ActionNotAllowedMessage');
        }
        ActionNotAllowedMessage.ClassName = 'ActionNotAllowedMessage';
        return ActionNotAllowedMessage;
    })(KMud.MessageBase);
    KMud.ActionNotAllowedMessage = ActionNotAllowedMessage;
    var ActorInformationMessage = (function (_super) {
        __extends(ActorInformationMessage, _super);
        function ActorInformationMessage() {
            _super.call(this, 'ActorInformationMessage');
        }
        ActorInformationMessage.ClassName = 'ActorInformationMessage';
        return ActorInformationMessage;
    })(KMud.MessageBase);
    KMud.ActorInformationMessage = ActorInformationMessage;
    var AmbiguousItemMessage = (function (_super) {
        __extends(AmbiguousItemMessage, _super);
        function AmbiguousItemMessage() {
            _super.call(this, 'AmbiguousItemMessage');
        }
        AmbiguousItemMessage.ClassName = 'AmbiguousItemMessage';
        return AmbiguousItemMessage;
    })(KMud.MessageBase);
    KMud.AmbiguousItemMessage = AmbiguousItemMessage;
    var AmbiguousActorMessage = (function (_super) {
        __extends(AmbiguousActorMessage, _super);
        function AmbiguousActorMessage() {
            _super.call(this, 'AmbiguousActorMessage');
        }
        AmbiguousActorMessage.ClassName = 'AmbiguousActorMessage';
        return AmbiguousActorMessage;
    })(KMud.MessageBase);
    KMud.AmbiguousActorMessage = AmbiguousActorMessage;
    var CommunicationMessage = (function (_super) {
        __extends(CommunicationMessage, _super);
        function CommunicationMessage() {
            _super.call(this, 'CommunicationMessage');
        }
        CommunicationMessage.ClassName = 'CommunicationMessage';
        return CommunicationMessage;
    })(KMud.MessageBase);
    KMud.CommunicationMessage = CommunicationMessage;
    var RemoveCommand = (function (_super) {
        __extends(RemoveCommand, _super);
        function RemoveCommand() {
            _super.call(this, 'RemoveCommand');
        }
        RemoveCommand.ClassName = 'RemoveCommand';
        return RemoveCommand;
    })(KMud.MessageBase);
    KMud.RemoveCommand = RemoveCommand;
    var EquipCommand = (function (_super) {
        __extends(EquipCommand, _super);
        function EquipCommand() {
            _super.call(this, 'EquipCommand');
        }
        EquipCommand.ClassName = 'EquipCommand';
        return EquipCommand;
    })(KMud.MessageBase);
    KMud.EquipCommand = EquipCommand;
    var ItemEquippedChangedMessage = (function (_super) {
        __extends(ItemEquippedChangedMessage, _super);
        function ItemEquippedChangedMessage() {
            _super.call(this, 'ItemEquippedChangedMessage');
        }
        ItemEquippedChangedMessage.ClassName = 'ItemEquippedChangedMessage';
        return ItemEquippedChangedMessage;
    })(KMud.MessageBase);
    KMud.ItemEquippedChangedMessage = ItemEquippedChangedMessage;
    var GenericMessage = (function (_super) {
        __extends(GenericMessage, _super);
        function GenericMessage() {
            _super.call(this, 'GenericMessage');
        }
        GenericMessage.ClassName = 'GenericMessage';
        return GenericMessage;
    })(KMud.MessageBase);
    KMud.GenericMessage = GenericMessage;
    var InventoryCommand = (function (_super) {
        __extends(InventoryCommand, _super);
        function InventoryCommand() {
            _super.call(this, 'InventoryCommand');
        }
        InventoryCommand.ClassName = 'InventoryCommand';
        return InventoryCommand;
    })(KMud.MessageBase);
    KMud.InventoryCommand = InventoryCommand;
    var InventoryListMessage = (function (_super) {
        __extends(InventoryListMessage, _super);
        function InventoryListMessage() {
            _super.call(this, 'InventoryListMessage');
        }
        InventoryListMessage.ClassName = 'InventoryListMessage';
        return InventoryListMessage;
    })(KMud.MessageBase);
    KMud.InventoryListMessage = InventoryListMessage;
    var GetItemCommand = (function (_super) {
        __extends(GetItemCommand, _super);
        function GetItemCommand() {
            _super.call(this, 'GetItemCommand');
        }
        GetItemCommand.ClassName = 'GetItemCommand';
        return GetItemCommand;
    })(KMud.MessageBase);
    KMud.GetItemCommand = GetItemCommand;
    var DropItemCommand = (function (_super) {
        __extends(DropItemCommand, _super);
        function DropItemCommand() {
            _super.call(this, 'DropItemCommand');
        }
        DropItemCommand.ClassName = 'DropItemCommand';
        return DropItemCommand;
    })(KMud.MessageBase);
    KMud.DropItemCommand = DropItemCommand;
    var ItemOwnershipMessage = (function (_super) {
        __extends(ItemOwnershipMessage, _super);
        function ItemOwnershipMessage() {
            _super.call(this, 'ItemOwnershipMessage');
        }
        ItemOwnershipMessage.ClassName = 'ItemOwnershipMessage';
        return ItemOwnershipMessage;
    })(KMud.MessageBase);
    KMud.ItemOwnershipMessage = ItemOwnershipMessage;
    var CashTransferMessage = (function (_super) {
        __extends(CashTransferMessage, _super);
        function CashTransferMessage() {
            _super.call(this, 'CashTransferMessage');
        }
        CashTransferMessage.ClassName = 'CashTransferMessage';
        return CashTransferMessage;
    })(KMud.MessageBase);
    KMud.CashTransferMessage = CashTransferMessage;
    var LoginRejected = (function (_super) {
        __extends(LoginRejected, _super);
        function LoginRejected() {
            _super.call(this, 'LoginRejected');
        }
        LoginRejected.ClassName = 'LoginRejected';
        return LoginRejected;
    })(KMud.MessageBase);
    KMud.LoginRejected = LoginRejected;
    var LoginStateMessage = (function (_super) {
        __extends(LoginStateMessage, _super);
        function LoginStateMessage() {
            _super.call(this, 'LoginStateMessage');
        }
        LoginStateMessage.ClassName = 'LoginStateMessage';
        return LoginStateMessage;
    })(KMud.MessageBase);
    KMud.LoginStateMessage = LoginStateMessage;
    var LookMessage = (function (_super) {
        __extends(LookMessage, _super);
        function LookMessage() {
            _super.call(this, 'LookMessage');
        }
        LookMessage.ClassName = 'LookMessage';
        return LookMessage;
    })(KMud.MessageBase);
    KMud.LookMessage = LookMessage;
    var MoveMessage = (function (_super) {
        __extends(MoveMessage, _super);
        function MoveMessage() {
            _super.call(this, 'MoveMessage');
        }
        MoveMessage.ClassName = 'MoveMessage';
        return MoveMessage;
    })(KMud.MessageBase);
    KMud.MoveMessage = MoveMessage;
    var PartyMovementMessage = (function (_super) {
        __extends(PartyMovementMessage, _super);
        function PartyMovementMessage() {
            _super.call(this, 'PartyMovementMessage');
        }
        PartyMovementMessage.ClassName = 'PartyMovementMessage';
        return PartyMovementMessage;
    })(KMud.MessageBase);
    KMud.PartyMovementMessage = PartyMovementMessage;
    var PingMessage = (function (_super) {
        __extends(PingMessage, _super);
        function PingMessage() {
            _super.call(this, 'PingMessage');
        }
        PingMessage.ClassName = 'PingMessage';
        return PingMessage;
    })(KMud.MessageBase);
    KMud.PingMessage = PingMessage;
    var PongMessage = (function (_super) {
        __extends(PongMessage, _super);
        function PongMessage() {
            _super.call(this, 'PongMessage');
        }
        PongMessage.ClassName = 'PongMessage';
        return PongMessage;
    })(KMud.MessageBase);
    KMud.PongMessage = PongMessage;
    var RoomDescriptionMessage = (function (_super) {
        __extends(RoomDescriptionMessage, _super);
        function RoomDescriptionMessage() {
            _super.call(this, 'RoomDescriptionMessage');
        }
        RoomDescriptionMessage.ClassName = 'RoomDescriptionMessage';
        return RoomDescriptionMessage;
    })(KMud.MessageBase);
    KMud.RoomDescriptionMessage = RoomDescriptionMessage;
    var SearchCommand = (function (_super) {
        __extends(SearchCommand, _super);
        function SearchCommand() {
            _super.call(this, 'SearchCommand');
        }
        SearchCommand.ClassName = 'SearchCommand';
        return SearchCommand;
    })(KMud.MessageBase);
    KMud.SearchCommand = SearchCommand;
    var SearchMessage = (function (_super) {
        __extends(SearchMessage, _super);
        function SearchMessage() {
            _super.call(this, 'SearchMessage');
        }
        SearchMessage.ClassName = 'SearchMessage';
        return SearchMessage;
    })(KMud.MessageBase);
    KMud.SearchMessage = SearchMessage;
    var ServerMessage = (function (_super) {
        __extends(ServerMessage, _super);
        function ServerMessage() {
            _super.call(this, 'ServerMessage');
        }
        ServerMessage.ClassName = 'ServerMessage';
        return ServerMessage;
    })(KMud.MessageBase);
    KMud.ServerMessage = ServerMessage;
    var SysopMessage = (function (_super) {
        __extends(SysopMessage, _super);
        function SysopMessage() {
            _super.call(this, 'SysopMessage');
        }
        SysopMessage.ClassName = 'SysopMessage';
        return SysopMessage;
    })(KMud.MessageBase);
    KMud.SysopMessage = SysopMessage;
    var ItemDescription = (function () {
        function ItemDescription() {
        }
        return ItemDescription;
    })();
    KMud.ItemDescription = ItemDescription;
    var ActorDescription = (function () {
        function ActorDescription() {
        }
        return ActorDescription;
    })();
    KMud.ActorDescription = ActorDescription;
    var CurrencyDescription = (function () {
        function CurrencyDescription() {
        }
        return CurrencyDescription;
    })();
    KMud.CurrencyDescription = CurrencyDescription;
    var ExitDescription = (function () {
        function ExitDescription() {
        }
        return ExitDescription;
    })();
    KMud.ExitDescription = ExitDescription;
    (function (CommunicationType) {
        CommunicationType[CommunicationType["Gossip"] = 0] = "Gossip";
        CommunicationType[CommunicationType["Auction"] = 1] = "Auction";
        CommunicationType[CommunicationType["Say"] = 2] = "Say";
        CommunicationType[CommunicationType["Yell"] = 3] = "Yell";
        CommunicationType[CommunicationType["Region"] = 4] = "Region";
        CommunicationType[CommunicationType["Gangpath"] = 5] = "Gangpath";
        CommunicationType[CommunicationType["Officerpath"] = 6] = "Officerpath";
        CommunicationType[CommunicationType["Chatroom"] = 7] = "Chatroom";
        CommunicationType[CommunicationType["Telepath"] = 8] = "Telepath";
    })(KMud.CommunicationType || (KMud.CommunicationType = {}));
    var CommunicationType = KMud.CommunicationType;
    (function (Direction) {
        Direction[Direction["North"] = 0] = "North";
        Direction[Direction["South"] = 1] = "South";
        Direction[Direction["East"] = 2] = "East";
        Direction[Direction["West"] = 3] = "West";
        Direction[Direction["Northeast"] = 4] = "Northeast";
        Direction[Direction["Northwest"] = 5] = "Northwest";
        Direction[Direction["Southeast"] = 6] = "Southeast";
        Direction[Direction["Southwest"] = 7] = "Southwest";
        Direction[Direction["Up"] = 8] = "Up";
        Direction[Direction["Down"] = 9] = "Down";
    })(KMud.Direction || (KMud.Direction = {}));
    var Direction = KMud.Direction;
    (function (LightLevel) {
        LightLevel[LightLevel["Daylight"] = 50] = "Daylight";
        LightLevel[LightLevel["Nothing"] = -10000] = "Nothing";
        LightLevel[LightLevel["PitchBlack"] = -500] = "PitchBlack";
        LightLevel[LightLevel["VeryDark"] = -250] = "VeryDark";
        LightLevel[LightLevel["BarelyVisible"] = -200] = "BarelyVisible";
        LightLevel[LightLevel["DimlyLit"] = -150] = "DimlyLit";
        LightLevel[LightLevel["RegularLight"] = -50] = "RegularLight";
    })(KMud.LightLevel || (KMud.LightLevel = {}));
    var LightLevel = KMud.LightLevel;
})(KMud || (KMud = {}));
