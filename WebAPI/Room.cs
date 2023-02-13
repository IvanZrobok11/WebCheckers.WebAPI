using Domain.Base.Classes;
using Domain.Base.Enums;
using Domain.Extension;

namespace WebAPI
{
    internal class RoomLibrary
    {
        private static RoomLibrary roomLibrary;
        public Dictionary<int, Room> Rooms;
        private RoomLibrary()
        {
            Rooms = new Dictionary<int, Room>();
        }

        public static RoomLibrary Instance
        {
            get
            {
                if (roomLibrary is null)
                {
                    roomLibrary = new RoomLibrary();
                }
                return roomLibrary;
            }
        }

        public Room CreateRoom(int playerId1, int? playerId2 = null)
        {
            CheckersPlayer? player2 = playerId2 is null ? null : new CheckersPlayer(playerId2.Value, CellColor.Black);
            var room = new Room(new CheckersPlayer(playerId1, CellColor.White), player2);

            Rooms.Add(GenerateId(), room);
            return room;
        }
        public void AddPlayer(int roomId, int playerId)
        {
            var room = Rooms.First(room => room.Key == roomId).Value;
            if (!room.IsFreePlace(out var freePlace)) return;

            if (room.Player1 == null)
            {
                room.Player1 = new CheckersPlayer(playerId, freePlace.Value);
            }
            else if (room.Player2 == null)
            {
                room.Player2 = new CheckersPlayer(playerId, freePlace.Value);
            }
        }

        private int GenerateId()
        {
            int id;
            do
            {
                id = Randomizer.RandomInt();
            } while (Rooms.ContainsKey(id));

            return id;
        }
    }
    internal class Room
    {
        public Room()
        {

        }
        public Room(CheckersPlayer player1, CheckersPlayer? player2 = null)
        {
            Player1 = player1;
            if (player2 != null)
            {
                Player2 = player2;
            }
        }

        public CheckersPlayer Player1 { get; set; }
        public CheckersPlayer Player2 { get; set; }

        public bool IsFreePlace(out CellColor? freePlace)
        {
            freePlace = null;
            if (Player1 != null && Player2 != null) return false;

            if (Player1 != null) freePlace = Player1.Color;
            else if (Player2 != null) freePlace = Player2.Color;

            return true;
        }

    }
}
