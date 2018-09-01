using Foodpoll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foodpoll.Facade
{
    public interface IFacade
    {
        //Shop
        IEnumerable<Shop> ListShop(string username);
        Shop CreateShop(Shop shop);
        DisplayShop GetShop(string shopId, string username);
        Shop AddMenu(Menu menu, string shopId);
        void DeleteShop(string shopId);
        Shop DeleteMenu(string shopId, string menuId);

        //poll
        Poll CreatePoll(string shopId);
        //IEnumerable<Poll> GetActivePoll(string username);
        void DeletePoll(string pollId);


        IEnumerable<PollDisplay> ListPoll(string username);

        PollDisplay GetPoll(string pollId, string username);

        //member
        IEnumerable<Member> ListMembers();
        Member GetMember(string id);
        Member AddMember(string name);
        void DeleteMember(string id);
        Member EditMember(string id, string newName);


        //setting
        Shop SetDefaulMenu(string shopid, string menuid);
        void SetMyDefaultShopMenu(string shopid, string menuid, string username);

        void SentOrder(string pollId, string menuid, string username);

        void ClosePoll(string pollId);

        IEnumerable<PollDisplay> ListClosePoll(string username);
    }
}
