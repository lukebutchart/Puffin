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
            Button resetButton = FindViewById<Button>(Resource.Id.ResetButton);

            List<string> allCollNames = new List<string>();
            List<string> allFriendNames = new List<string>();

            foreach (Collectable coll in mydata.GetAllCollectables())
            {
                allCollNames.Add(coll.Name);
            }

            foreach (Collectable coll in AppManager.Collectables)
            {
                if (!allCollNames.Contains(coll.Name))
                {
                    mydata.AddCollectable(coll);
                }
            }

            foreach (Friend friend in mydata.GetAllFriends())
            {
                allFriendNames.Add(friend.Name);
            }

            foreach (Friend friend in AppManager.Friends)
            {
                if (!allFriendNames.Contains(friend.Name))
                {
                    mydata.AddFriend(friend);
                }
            }

            mydata.AddPlayer(new Player(AppManager.PlayerName));

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

            resetButton.Click += (sender, e) =>
            {
                AlertDialog.Builder alert = new AlertDialog.Builder(this);

                alert.SetTitle(String.Format("Are you sure you want to reset all data?"));

                alert.SetPositiveButton("Yes", (senderAlert, args) => {
                    foreach (Collectable coll in AppManager.DBUpdates.GetAllCollectables())
                    {
                        AppManager.DBUpdates.DeleteCollectable(coll);
                    }
                    foreach (Player player in AppManager.DBUpdates.GetAllPlayers())
                    {
                        AppManager.DBUpdates.DeletePlayer(player);
                    }
                    foreach (Friend friend in AppManager.DBUpdates.GetAllFriends())
                    {
                        AppManager.DBUpdates.DeleteFriend(friend);
                    }

                    foreach (Collectable coll in AppManager.Collectables)
                    {
                        mydata.AddCollectable(coll);
                    }
                    mydata.AddPlayer(new Player(AppManager.PlayerName));
                    foreach (Friend friend in AppManager.Friends)
                    {
                        mydata.AddFriend(friend);
                    }
                });

                alert.SetNegativeButton("No", (senderAlert, args) => {                    
                });

                Dialog dialog = alert.Create();
                dialog.Show();
            };
        }
        

        //protected override void OnResume(Bundle bundle)
        //{
        //}

        //protected override void OnPause(Bundle bundle)
        //{
        //}
    }
}