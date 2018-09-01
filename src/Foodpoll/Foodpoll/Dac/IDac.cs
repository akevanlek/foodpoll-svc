using Foodpoll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Foodpoll.Dac
{
    public interface IDac
    {
        IEnumerable<Shop> ListShop(Expression<Func<Shop, bool>> expression);
        Shop GetShop(Expression<Func<Shop, bool>> expression);

        Poll GetPoll(Expression<Func<Poll, bool>> expression);
        IEnumerable<Poll> ListPoll(Expression<Func<Poll, bool>> expression);

        void CreateShop(Shop newshop);
        void UpdateShop(Shop shop);

        void CreatePoll(Poll newPoll);
        void UpdatePoll(Poll poll);

        Member GetMember(Expression<Func<Member, bool>> expression);
        IEnumerable<Member> ListMember(Expression<Func<Member, bool>> expression);
        void CreateMember(Member newmember);
        void UpdateMember(Member member);


   
    }
}
