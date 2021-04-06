using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;

namespace weatherApp
{
	[Activity(Label = "CitiesActivity")]
	public class CitiesActivity : Activity
	{
		private TextView Chrzin;
		private TextView Hrensko;
		private TextView Jesenik;
		private TextView Praha;
		private TextView Melnik;


		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			Xamarin.Essentials.Platform.Init(this, savedInstanceState);
			SetContentView(Resource.Layout.CitiesLayout);
			setupReferences();
			subscribeEventHandlers();
		}

		private void subscribeEventHandlers()
		{
			Chrzin.Click +=chrzin_Click;
			Hrensko.Click += hrensko_Click;
			Jesenik.Click += jesenik_Click;
			Praha.Click += praha_Click;
			Melnik.Click += melnik_Click;
		}

		private void melnik_Click(object sender, EventArgs e)
		{
			goBackWithMessage("Melnik");
			System.Diagnostics.Debug.WriteLine("Melník");
		}

		private void goBackWithMessage(string city)
		{
			Intent data = new Intent();
			data.PutExtra("City", city);
			data.PutExtra("Cislo", 1);
			SetResult(Result.Ok, data);
			Finish();
		}

		private void praha_Click(object sender, EventArgs e)
		{
			goBackWithMessage("Praha");
			System.Diagnostics.Debug.WriteLine("Praha");
		}

		private void jesenik_Click(object sender, EventArgs e)
		{
			goBackWithMessage("Jesenik");
			System.Diagnostics.Debug.WriteLine("Jeseník");
		}

		private void hrensko_Click(object sender, EventArgs e)
		{
			goBackWithMessage("Hrensko");
			System.Diagnostics.Debug.WriteLine("Hřensko");
		}

		private void chrzin_Click(object sender, EventArgs e)
		{
			goBackWithMessage("Chrzin");
			System.Diagnostics.Debug.WriteLine("Chržín");
		}

		private void setupReferences()
		{
			Chrzin = FindViewById<TextView>(Resource.Id.lbChrzin);
			Hrensko = FindViewById<TextView>(Resource.Id.lbHrensko);
			Jesenik = FindViewById<TextView>(Resource.Id.lbJesenik);
			Praha = FindViewById<TextView>(Resource.Id.lbPraha);
			Melnik = FindViewById<TextView>(Resource.Id.lbMnelnik);
		}
	}
}