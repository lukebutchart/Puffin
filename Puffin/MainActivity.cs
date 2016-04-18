using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System;

namespace Puffin
{
    [Activity(Label = "Puffin", MainLauncher = true, Icon = "@drawable/Puffin")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            DatabaseUpdates mydata = AppManager.DBUpdates;

            mydata.SetContext(this);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button playButton = FindViewById<Button>(Resource.Id.PlayButton);
            Button collectionButton = FindViewById<Button>(Resource.Id.CollectionButton);
            Button shopButton = FindViewById<Button>(Resource.Id.ShopButton);
            Button claimCoinsButton = FindViewById<Button>(Resource.Id.ClaimCoinsButton);
            Button resetButton = FindViewById<Button>(Resource.Id.ResetButton);

            List<string> allCollNames = new List<string>();
            List<string> allFriendNames = new List<string>();

            //foreach (Collectable coll in mydata.GetAllCollectables())
            //{
            //    allCollNames.Add(coll.Name);
            //}

            //foreach (Collectable coll in AppManager.Collectables)
            //{
            //    if (!allCollNames.Contains(coll.Name))
            //    {
            //        mydata.AddCollectable(coll);
            //    }
            //}

            //foreach (Friend friend in mydata.GetAllFriends())
            //{
            //    allFriendNames.Add(friend.Name);
            //}

            //foreach (Friend friend in AppManager.Friends)
            //{
            //    if (!allFriendNames.Contains(friend.Name))
            //    {
            //        mydata.AddFriend(friend);
            //    }
            //}

            //mydata.AddPlayer(new Player(AppManager.PlayerName));

            PopulateTable<Collectable>(mydata);
            PopulateTable<Player>(mydata);
            PopulateTable<Friend>(mydata);

            playButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(Puffin.PlayActivity));
                //intent.PutStringArrayListExtra("phone_numbers", phoneNumbers);
                StartActivity(intent);
            };

            collectionButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(Puffin.CollectionActivity));
                StartActivity(intent);
            };

            shopButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(Puffin.ShopActivity));
                StartActivity(intent);
            };

            claimCoinsButton.Click += (sender, e) =>
            {
                
            };

            resetButton.Click += (sender, e) =>
            {
                AlertDialog.Builder alert = new AlertDialog.Builder(this);

                alert.SetTitle(string.Format("Are you sure you want to reset all data?"));

                alert.SetPositiveButton("Yes", (senderAlert, args) =>
                {
                    //foreach (Collectable coll in AppManager.DBUpdates.GetAllCollectables())
                    //{
                    //    AppManager.DBUpdates.DeleteCollectable(coll);
                    //}
                    //foreach (Player player in AppManager.DBUpdates.GetAllPlayers())
                    //{
                    //    AppManager.DBUpdates.DeletePlayer(player);
                    //}
                    //foreach (Friend friend in AppManager.DBUpdates.GetAllFriends())
                    //{
                    //    AppManager.DBUpdates.DeleteFriend(friend);
                    //}
                    //foreach (Collectable coll in AppManager.Collectables)
                    //{
                    //    mydata.AddCollectable(coll);
                    //}
                    //mydata.AddPlayer(new Player(AppManager.PlayerName));
                    //foreach (Friend friend in AppManager.Friends)
                    //{
                    //    mydata.AddFriend(friend);
                    //}

                    ClearTable<Collectable>();
                    ClearTable<Player>();
                    ClearTable<Friend>();

                    PopulateTable<Collectable>(mydata);
                    PopulateTable<Player>(mydata);
                    PopulateTable<Friend>(mydata);
                });

                alert.SetNegativeButton("No", (senderAlert, args) => {                    
                });

                Dialog dialog = alert.Create();
                dialog.Show();
            };


            Xamarin.Forms.Forms.Init(this, bundle);
            var timer = new System.Threading.Timer(e => OnTimer(), null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
        }

        void OnTimer()
        {
            int coin = AppManager.ClaimCoin;

            if (AppManager.ClaimCoin < 10)
            {
                AppManager.ClaimCoin++;

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    FindViewById<TextView>(Resource.Id.ClaimCoinsButton).Text = String.Format("Claim {0} coins", coins.ToString());
                });
            }

            //Player player = AppManager.DBUpdates.GetPlayer(AppManager.PlayerName);

            //player.Coins++;
            //AppManager.DBUpdates.UpdatePlayer(player);

            //Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            //{
            //    FindViewById<TextView>(Resource.Id.ClaimCoinsButton).Text =  String.Format("Claim {0} coins", AppManager.DBUpdates.GetPlayer(AppManager.PlayerName).Coins.ToString());
            //});
        }

        private static void PopulateTable<T>(DatabaseUpdates mydata)
        {
            List<string> allNames = new List<string>();

            if (typeof(T) == typeof(Collectable))
            {
                foreach (Collectable coll in mydata.GetAllCollectables())
                {
                    allNames.Add(coll.Name);
                }
                foreach (Collectable coll in AppManager.Collectables)
                {
                    if (!allNames.Contains(coll.Name))
                    {
                        mydata.AddCollectable(coll);
                    }
                }
            }
            if (typeof(T) == typeof(Player))
            {
                foreach (Player player in mydata.GetAllPlayers())
                {
                    allNames.Add(player.Name);
                }
                if (!allNames.Contains(AppManager.PlayerName))
                {
                    mydata.AddPlayer(new Player(AppManager.PlayerName));
                }
            }
            if (typeof(T) == typeof(Friend))
            {
                foreach (Friend friend in mydata.GetAllFriends())
                {
                    allNames.Add(friend.Name);
                }
                foreach (Friend friend in AppManager.Friends)
                {
                    if (!allNames.Contains(friend.Name))
                    {
                        mydata.AddFriend(friend);
                    }
                }
            }            
        }

        private static void ClearTable<T>()
        {
            if (typeof(T) == typeof(Collectable))
            {
                foreach (Collectable coll in AppManager.DBUpdates.GetAllCollectables())
                {
                    AppManager.DBUpdates.DeleteCollectable(coll);
                }
            }
            if (typeof(T) == typeof(Player))
            {
                foreach (Player player in AppManager.DBUpdates.GetAllPlayers())
                {
                    AppManager.DBUpdates.DeletePlayer(player);
                }
            }
            if (typeof(T) == typeof(Friend))
            {
                foreach (Friend friend in AppManager.DBUpdates.GetAllFriends())
                {
                    AppManager.DBUpdates.DeleteFriend(friend);
                }
            }
        }


        //protected override void OnResume(Bundle bundle)
        //{
        //}

        //protected override void OnPause(Bundle bundle)
        //{
        //}
    }
}