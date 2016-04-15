using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;

namespace Puffin
{
    [Activity(Label = "Puffin", MainLauncher = true, Icon = "@drawable/icon")]
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

            List<string> allCollNames = new List<string>();

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
        }





        //protected override void OnResume(Bundle bundle)
        //{
        //}

        //protected override void OnPause(Bundle bundle)
        //{
        //}
    }
}