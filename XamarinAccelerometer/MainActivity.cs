using Android.App;
using Android.Widget;
using Android.OS;
using Android.Hardware;
using Android.Content;

namespace XamarinAccelerometer
{
	[Activity(Label = "XamarinAccelerometer", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity, ISensorEventListener
	{
		static readonly object syncLock = new object();
		SensorManager _sm;
		TextView _vw;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			SetContentView (Resource.Layout.Main);
			_sm = (SensorManager)GetSystemService(Context.SensorService);
			_vw = FindViewById<TextView>(Resource.Id.accelerometer_text);
		}

		protected override void OnResume()
		{
			base.OnResume();
			_sm.RegisterListener(this, _sm.GetDefaultSensor(SensorType.Accelerometer), SensorDelay.Ui);
		}
		protected override void OnPause()
		{
			base.OnPause();
			_sm.UnregisterListener(this);
		}

		public void OnAccuracyChanged(Sensor sensor, SensorStatus accuracy)
		{
		}

		public void OnSensorChanged(SensorEvent e)
		{
			lock (syncLock)
			{
				_vw.Text = string.Format("x={0:f}, y={1:f}, z={2:f}", e.Values[0], e.Values[1], e.Values[2]);
			}
		}
	}
}

