using System;
using Xamarin.Essentials;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.Widget;
using AndroidX.AppCompat.App;
using Android.Graphics;
using Android.Widget;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Snackbar;
using Android.Content;
using Android.App;
using Shared;
using System.Threading.Tasks;
using System.Threading;

namespace weatherApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity , iMainView
    {
        string coordinates;
        private Button btChangeCity;
        private Button btFind;
        private ImageView imgCR;
        private ImageView imgLocation;
        private TextView lbCity;
        private TextView lbTempValue;
        private TextView lbMoistureValue;
        private TextView lbWindValue;
        private TextView lbRegionCountry;
        private TextView lbWeather;
        private TextView lbPrecipitation;
        private TextView lbPrecipValue;
        private TextView lbLocation;
        private TextView lbLocationTemperature;
        private TextView lbYourLocation;
        private EditText tbText;


        private iMainView mainview;
        

        protected override void OnCreate(Android.OS.Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.layout1);
            setupReferences();
            subscribeEventHandlers();
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            GetCurrentLocation();
        }
  
		private void subscribeEventHandlers()
		{
            btChangeCity.Click += btChangeCity_Click;
            btFind.Click += btFind_Click;
            imgLocation.Click += imgLocation_Click;
        }

		private void btFind_Click(object sender, EventArgs e)
		{
            WeatherService ws = new WeatherService(this);
            ws.getWeatherInfo(tbText.Text);

        }

		private void imgLocation_Click(object sender, EventArgs e)
		{
		    GetCurrentLocation();
           
        }

		private void btChangeCity_Click(object sender, EventArgs e)
		{
			StartCitiesActivity();
		}

		private void StartCitiesActivity()
		{
            var intent = new Intent(this, typeof(CitiesActivity));
            StartActivityForResult(intent,1);
        }
        
		protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent intent)
		{
			base.OnActivityResult(requestCode, resultCode, intent);
            if (resultCode == Result.Ok)
			{
                WeatherService ws = new WeatherService(this);
                var city = intent.GetStringExtra("City");
                ws.getWeatherInfo(city);
			}
		}
		private void setupReferences()
		{
            btChangeCity = FindViewById<Button>(Resource.Id.btnChangeCity);
            btFind = FindViewById<Button>(Resource.Id.btnFind);
            imgCR = FindViewById<ImageView>(Resource.Id.imgCR);
            imgLocation = FindViewById<ImageView>(Resource.Id.imgLocation);
            lbCity = FindViewById<TextView>(Resource.Id.lbCity);
            lbTempValue = FindViewById<TextView>(Resource.Id.lbTempValue);
            lbMoistureValue = FindViewById<TextView>(Resource.Id.lbMoistureValue);
            lbWindValue = FindViewById<TextView>(Resource.Id.lbWindValue);
            lbRegionCountry= FindViewById<TextView>(Resource.Id.lbRegionCountry);
            lbPrecipitation = FindViewById<TextView>(Resource.Id.lbPrecip);
            lbPrecipValue = FindViewById<TextView>(Resource.Id.lbPrecipValue);
            lbLocation = FindViewById<TextView>(Resource.Id.lbLocation);
            lbYourLocation = FindViewById<TextView>(Resource.Id.lbYourLocation);
            lbLocationTemperature= FindViewById<TextView>(Resource.Id.lbLocationTemperature);
            tbText = FindViewById<EditText>(Resource.Id.tbText);

        }
        //public override bool OnCreateOptionsMenu(IMenu menu)
        //{
        //    MenuInflater.Inflate(Resource.Menu.menu_main, menu);
        //    return true;
        //}

        

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

		public void SetWeatherData(Shared.WeatherModel model)
		{
            lbCity.Text = model.location.name;
            lbRegionCountry.Text = "(" + model.location.region + "/" + model.location.country + ")";
            lbTempValue.Text = model.current.temperature.ToString()+ " °C";
            lbWindValue.Text = model.current.wind_speed.ToString()+" m/s";
            lbMoistureValue.Text = model.current.humidity.ToString()+"%";
        }
        public void SetLocation(Shared.WeatherModel model)
        {
            lbCity.Text = model.location.name;
            lbLocationTemperature.Text = model.current.temperature.ToString() + " °C";
            lbLocation.Text= model.location.name + " / ";

        }
        CancellationTokenSource cts;
        async Task GetCurrentLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);
                
                if (location != null)
                {
					var coordinates = $"{location.Latitude},{location.Longitude}";
                    WeatherService current_location = new WeatherService(this);
                    current_location.getWeatherInfo(coordinates);
                    current_location.getFirstInfo(coordinates);
                    System.Diagnostics.Debug.WriteLine(coordinates);

                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }

        protected void OnDisappearing()
        {
            if (cts != null && !cts.IsCancellationRequested)
                cts.Cancel();
            this.OnDisappearing();
        }
        public void SetPicture(int code)
        {

            switch (code)
            {
                case 113:
                    imgCR.SetImageResource(Resource.Drawable.sunny);
                    break;
                case 116:
                    imgCR.SetImageResource(Resource.Drawable.cloudySunnny);
                    break;
                case 119:
                    imgCR.SetImageResource(Resource.Drawable.cloudy);
                    break;
                case 122:
                    imgCR.SetImageResource(Resource.Drawable.cloudy);
                    break;
                case 143:
                    imgCR.SetImageResource(Resource.Drawable.foggy);
                    break;
                case 176:
                    imgCR.SetImageResource(Resource.Drawable.littleRaining);
                    break;
                case 179:
                    imgCR.SetImageResource(Resource.Drawable.cloudSnow);
                    break;
                case 182:
                    imgCR.SetImageResource(Resource.Drawable.cloudSnow);
                    break;
                case 185:
                    imgCR.SetImageResource(Resource.Drawable.littleRaining);
                    break;
                case 200:
                    imgCR.SetImageResource(Resource.Drawable.storm);
                    break;
                case 201:
                    imgCR.SetImageResource(Resource.Drawable.nightStorm);
                    break;
                case 183:
                    imgCR.SetImageResource(Resource.Drawable.cloudSnow);
                    break;
                case 186:
                    imgCR.SetImageResource(Resource.Drawable.nightRaining);
                    break;
            }
            
        }
	}
}
