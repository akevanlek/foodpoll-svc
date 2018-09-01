using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foodpoll.Facade;
using Foodpoll.Models;
using Microsoft.AspNetCore.Mvc;

namespace Foodpoll.Controllers
{
    [Route("api/[controller]")]
    public class FoodpollController : Controller
    {
        private IFacade facade;
        public FoodpollController(IFacade facade)
        {
            this.facade = facade;
        }

        [Route("GetShop/{shopId}/{username}")]
        [HttpGet]
        public DisplayShop GetShop(string shopId, string username)
        {
            return facade.GetShop(shopId, username);
        }

        [Route("ListShop/{username}")]
        [HttpGet]
        public IEnumerable<Shop> ListShop(string username)
        {
            return facade.ListShop(username);
        }

        [Route("CreateShop")]
        [HttpPost]
        public Shop CreateShop([FromBody]Shop shop)
        {
            return facade.CreateShop(shop);
        }

        [Route("AddMenu/{username}")]
        [HttpPost]
        public Shop AddMenu([FromBody]AddMenuRequest request, string username)
        {
            return facade.AddMenu(request.Menu, request.ShopId);
        }

        [Route("DeleteShop/{shopid}")]
        [HttpGet]
        public void DeleteShop(string shopid)
        {
            facade.DeleteShop(shopid);
        }

        [Route("DeleteMenu/{shopid}/{menuId}")]
        [HttpGet]
        public Shop DeleteMenu(string shopid, string menuId)
        {
            return facade.DeleteMenu(shopid, menuId);
        }

        [Route("CreatePoll/{shopid}")]
        [HttpGet]
        public Poll CreatePoll(string shopid)
        {
            return facade.CreatePoll(shopid);
        }

        [Route("GetActivePoll/{username}")]
        [HttpGet]
        public IEnumerable<PollDisplay> GetActivePoll(string username)
        {
            return facade.ListPoll(username);
        }

        [Route("GetPoll/{pollId}/{username}")]
        [HttpGet]
        public PollDisplay GetPoll(string pollId, string username)
        {
            return facade.GetPoll(pollId, username);
        }


        [Route("DeletePoll/{pollId}")]
        [HttpGet]
        public void DeletePoll(string pollId)
        {
            facade.DeletePoll(pollId);
        }

        [Route("ListMembers")]
        [HttpGet]
        public IEnumerable<Member> ListMembers()
        {
            return facade.ListMembers();
        }

        [Route("GetMember/{id}")]
        [HttpGet]
        public Member GetMember(string id)
        {
            return facade.GetMember(id);
        }

        [Route("AddMember/{name}")]
        [HttpGet]
        public Member AddMember(string name)
        {
            return facade.AddMember(name);
        }

        [Route("DeleteMember/{id}")]
        [HttpGet]
        public void DeleteMember(string id)
        {
            facade.DeleteMember(id);
        }

        [Route("EditMember/{id}/{newName}")]
        [HttpGet]
        public Member EditMember(string id, string newName)
        {
            return facade.EditMember(id, newName);
        }


        [Route("SetDefaulMenu/{shopid}/{menuid}")]
        [HttpGet]
        public Shop SetDefaulMenu(string shopid, string menuid)
        {
            return facade.SetDefaulMenu(shopid, menuid);
        }

        [Route("SetMyDefaultShopMenu/{shopid}/{menuid}/{username}")]
        [HttpGet]
        public void SetMyDefaultShopMenu(string shopid, string menuid, string username)
        {
            facade.SetMyDefaultShopMenu(shopid, menuid, username);
        }


        [Route("SentOrder/{pollId}/{menuid}/{username}")]
        [HttpGet]
        public void SentOrder(string pollId, string menuid, string username)
        {
            facade.SentOrder(pollId, menuid, username);
        }


        [Route("ClosePoll/{pollid}")]
        [HttpGet]
        public void ClosePoll(string pollid)
        {
            facade.ClosePoll(pollid);
        }


        [Route("ListClosePoll/{username}")]
        [HttpGet]
        public IEnumerable<PollDisplay> ListClosePoll(string username)
        {
            return facade.ListClosePoll(username);
        }





    }
}
