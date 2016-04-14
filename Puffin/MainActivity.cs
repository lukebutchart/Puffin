using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;

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

            foreach (Collectable coll in AppManager.Collectables)
            {
                mydata.AddCollectable(coll);
            }


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