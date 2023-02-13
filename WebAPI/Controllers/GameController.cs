using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("Game")]
    public class GameController : ControllerBase
    {
        [HttpGet(Name = "CreateRoom")]
        public void CreateRoom(int userId, int roomId)
        {
            //RoomLibrary.Instance.CreateRoom(userId);
            throw new Exception("Some exception");
        }

        //[HttpGet(Name = "AntendToRoom")]
        //public void AntendToRoom(int userId, int roomId)
        //{
        //    RoomLibrary.Instance.AddPlayer(roomId, userId);
        //}
    }
}
