using Foodpoll.Dac;
using Foodpoll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foodpoll.Facade
{
    public class Facade : IFacade
    {
        private IDac dac;
        public Facade(IDac dac)
        {
            this.dac = dac;
        }

        public Shop AddMenu(Menu menu, string shopId)
        {
            var shop = dac.GetShop(x => x._id == shopId);

            var shopmenu = shop.Menus.ToList();

            menu._id = Guid.NewGuid().ToString();
            menu.CreateDate = DateTime.UtcNow;

            shopmenu.Add(menu);

            shop.Menus = shopmenu;

            dac.UpdateShop(shop);

            return shop;
        }

        public Poll CreatePoll(string shopId)
        {
            var shop = dac.GetShop(x => x._id == shopId);
            Poll newpoll = new Poll
            {
                _id = Guid.NewGuid().ToString(),
                ShopId = shop._id,
                CloseDate = null,
                CreateDate = DateTime.UtcNow,
                Orders = new List<Order>(),
                DeleteDate = null
            };
            dac.CreatePoll(newpoll);

            return newpoll;

        }

        public Shop CreateShop(Shop shop)
        {
            shop._id = Guid.NewGuid().ToString();
            shop.CreateDate = DateTime.UtcNow;
            shop.Menus = new List<Menu>();
            shop.DefaultMenu = new Menu();
            dac.CreateShop(shop);

            return shop;
        }

        public Shop DeleteMenu(string shopId, string menuId)
        {
            var shop = dac.GetShop(x => x._id == shopId);

            var shopmenu = shop.Menus.ToList();

            var menu = shopmenu.Where(x => x._id == menuId).FirstOrDefault();

            shopmenu.Remove(menu);


            shop.Menus = shopmenu;

            dac.UpdateShop(shop);

            return shop;

        }

        public void DeleteShop(string shopId)
        {
            var shop = dac.GetShop(x => x._id == shopId);
            shop.DeleteDate = DateTime.UtcNow;

            dac.UpdateShop(shop);
        }

        public DisplayShop GetShop(string shopId, string username)
        {
            var shop = dac.GetShop(x => x._id == shopId);
            var member = dac.GetMember(x => x.Name == username);
            var mydefault = member.ShopMenus.Where(x => x._id == shop._id).FirstOrDefault();

            DisplayShop dpshop = new DisplayShop()
            {
                Shop = shop,
                MyDefaultMenu = mydefault.DefaultMenu,
            };


            return dpshop;
        }

        public IEnumerable<Shop> ListShop(string username)
        {
            var shopList = dac.ListShop(x => !x.DeleteDate.HasValue) ?? new List<Shop>();
            return shopList.ToList();
        }

        public void DeletePoll(string pollId)
        {
            var poll = dac.GetPoll(x => x._id == pollId);
            poll.DeleteDate = DateTime.UtcNow;

            dac.UpdatePoll(poll);
        }

        public IEnumerable<Member> ListMembers()
        {
            var listmember = dac.ListMember(x => !x.DeleteDate.HasValue).ToList();

            return listmember;
        }

        public Member AddMember(string name)
        {
            Member newMember = new Member
            {
                _id = Guid.NewGuid().ToString(),
                DeleteDate = null,
                CreateDate = DateTime.UtcNow,
                Name = name,
                ShopMenus = new List<ShopMenu>(),
            };

            dac.CreateMember(newMember);

            return newMember;
        }

        public void DeleteMember(string id)
        {
            var member = dac.GetMember(x => x._id == id);
            member.DeleteDate = DateTime.UtcNow;

            dac.UpdateMember(member);

        }

        public Member EditMember(string id, string newName)
        {
            var member = dac.GetMember(x => x._id == id);
            member.Name = newName;
            dac.UpdateMember(member);

            return member;
        }

        public Member GetMember(string id)
        {
            var member = dac.GetMember(x => x._id == id);

            return member;
        }

        public Shop SetDefaulMenu(string shopid, string menuid)
        {
            var shop = dac.GetShop(x => x._id == shopid);

            var menu = shop.Menus.Where(x => x._id == menuid).FirstOrDefault();
            shop.DefaultMenu = menu;

            dac.UpdateShop(shop);

            return shop;
        }

        public void SetMyDefaultShopMenu(string shopid, string menuid, string username)
        {
            var member = dac.GetMember(x => x.Name == username);

            var shop = dac.GetShop(x => x._id == shopid);
            var menu = shop.Menus.Where(x => x._id == menuid).FirstOrDefault();

            var shopmenu = member.ShopMenus.Where(x => x.Shop._id == shopid).FirstOrDefault();

            if (shopmenu != null)
            {
                shopmenu.DefaultMenu = menu;

                dac.UpdateMember(member);
            }
            else
            {
                ShopMenu newShopMenu = new ShopMenu()
                {
                    _id = Guid.NewGuid().ToString(),
                    Shop = shop,
                    DefaultMenu = menu,
                };

                var newDefaultMenu = member.ShopMenus.ToList();

                newDefaultMenu.Add(newShopMenu);

                member.ShopMenus = newDefaultMenu;

                dac.UpdateMember(member);

            }

        }

        public IEnumerable<PollDisplay> ListPoll(string username)
        {

            List<PollDisplay> displaypolls = new List<PollDisplay>();
            var polls = dac.ListPoll(x => !x.DeleteDate.HasValue && !x.CloseDate.HasValue);
            var member = dac.GetMember(x => x.Name == username);

            foreach (var item in polls)
            {
                var shop = dac.GetShop(x => x._id == item.ShopId);
                var mydefault = member.ShopMenus.Where(x => x._id == shop._id).FirstOrDefault();
                var myorder = item.Orders?.Where(x => x.Username == username).FirstOrDefault();

                PollDisplay dpPoll = new PollDisplay()
                {
                    _id = item._id,
                    Shop = shop,
                    DefaultMenu = shop.DefaultMenu,
                    MyDefaultMenu = mydefault?.DefaultMenu ?? new Menu(),
                    Orders = item.Orders,
                    CreateDate = item.CreateDate,
                    MyOrder = myorder == null ? new Menu() : myorder.Menu,
                };

                displaypolls.Add(dpPoll);
            }

            return displaypolls.ToList();
        }

        public PollDisplay GetPoll(string pollId, string username)
        {
            var poll = dac.GetPoll(x => x._id == pollId);
            var member = dac.GetMember(x => x.Name == username);

            var shop = dac.GetShop(x => x._id == poll.ShopId);
            var mydefault = member.ShopMenus?.Where(x => x._id == shop._id).FirstOrDefault();
            var myorder = poll.Orders?.Where(x => x.Username == username).FirstOrDefault();

            PollDisplay dpPoll = new PollDisplay()
            {
                _id = poll._id,
                Shop = shop,
                DefaultMenu = shop.DefaultMenu,
                MyDefaultMenu = mydefault?.DefaultMenu ?? new Menu(),
                Orders = poll.Orders,
                CreateDate = poll.CreateDate,
                MyOrder = myorder == null ? new Menu() : myorder.Menu,
            };

            return dpPoll;
        }

        public void SentOrder(string pollId, string menuid, string username)
        {
            var poll = dac.GetPoll(x => x._id == pollId);
            var shop = dac.GetShop(x => x._id == poll.ShopId);

            var mymenu = shop.Menus.Where(x => x._id == menuid).FirstOrDefault();

            var myorder = poll.Orders?.Where(x => x.Username == username).FirstOrDefault();
            if (myorder == null)
            {
                Order neworder = new Order()
                {
                    _id = Guid.NewGuid().ToString(),
                    Menu = mymenu,
                    Username = username,
                };

                var listOrder = poll.Orders.ToList();
                listOrder.Add(neworder);

                poll.Orders = listOrder;

                dac.UpdatePoll(poll);
            }
            else
            {
                myorder.Menu = mymenu;

                dac.UpdatePoll(poll);

            }

        }

        public void ClosePoll(string pollId)
        {
            var poll = dac.GetPoll(x => x._id == pollId);
            var listmember = dac.ListMember(x => true);
          
            var shop = dac.GetShop(x => x._id == poll.ShopId);

            List<Order> reOrderList = new List<Order>();
            foreach (var item in listmember)
            {
                var member = dac.GetMember(x => x.Name == item.Name);
                var order = poll.Orders.Where(x => x.Username == item.Name).FirstOrDefault();

                if (order == null)
                {
                    var mydefault = member.ShopMenus.Where(x => x._id == poll.ShopId).FirstOrDefault();
                    if (mydefault == null)
                    {
                        if (shop.DefaultMenu != null)
                        {
                            Order newOrder = new Order()
                            {
                                _id = Guid.NewGuid().ToString(),
                                Menu = shop.DefaultMenu,
                                Username = item.Name,
                            };
                            reOrderList.Add(newOrder);
                        }
                        else
                        {
                            Order newOrder = new Order()
                            {
                                _id = Guid.NewGuid().ToString(),
                                Menu = shop.Menus.FirstOrDefault(),
                                Username = item.Name,
                            };
                            reOrderList.Add(newOrder);
                        }
                    }
                    else
                    {
                        Order newOrder = new Order()
                        {
                            _id = Guid.NewGuid().ToString(),
                            Menu = mydefault.DefaultMenu,
                            Username = item.Name,
                        };
                        reOrderList.Add(newOrder);
                    }
                }
                else
                {
                    reOrderList.Add(order);
                }

                poll.Orders = reOrderList;
                poll.CloseDate = DateTime.UtcNow;
                dac.UpdatePoll(poll);
            }



        }

        public IEnumerable<PollDisplay> ListClosePoll(string username)
        {
            List<PollDisplay> displaypolls = new List<PollDisplay>();
            var polls = dac.ListPoll(x => x.CloseDate.HasValue);
            var member = dac.GetMember(x => x.Name == username);

            foreach (var item in polls)
            {
                var shop = dac.GetShop(x => x._id == item.ShopId);
                var mydefault = member.ShopMenus.Where(x => x._id == shop._id).FirstOrDefault();
                var myorder = item.Orders?.Where(x => x.Username == username).FirstOrDefault();

                PollDisplay dpPoll = new PollDisplay()
                {
                    _id = item._id,
                    Shop = shop,
                    DefaultMenu = shop.DefaultMenu,
                    MyDefaultMenu = mydefault?.DefaultMenu ?? new Menu(),
                    Orders = item.Orders,
                    CreateDate = item.CreateDate,
                    MyOrder = myorder == null ? new Menu() : myorder.Menu,
                };

                displaypolls.Add(dpPoll);
            }

            return displaypolls.ToList();
        }
    }
}
