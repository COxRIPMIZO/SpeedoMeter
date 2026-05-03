# SpeedoMeter Examples Project

This directory contains comprehensive examples demonstrating how to use the SpeedoMeter control in various scenarios.

## 📁 File Structure

```
Examples/
├── BasicGaugeExample.cs           # Simple gauge setup
├── RPMGaugeExample.cs             # Car RPM dashboard
├── CPUMonitorExample.cs           # System monitoring
├── SpeedometerDashboardExample.cs # Vehicle speedometer
├── ThemesExample.cs               # Color themes
├── AnimationExample.cs            # Value animations
├── MultiGaugeLayoutExample.cs     # Multiple gauges
└── CustomThemeExample.cs          # Custom styling
```

## 🚀 Running Examples

1. Add example file to SpeedoMeter project
2. Set as Startup Form in Program.cs
3. Build and run (__F5__)

## 📚 Example Descriptions

Each example demonstrates specific features and patterns.

---

## Example 1: BasicGaugeExample.cs

Simplest possible gauge setup - perfect for getting started.

```csharp
using System;
using System.Windows.Forms;

namespace SpeedoMeter.Examples
{
    public partial class BasicGaugeExample : Form
    {
        public BasicGaugeExample()
        {
            InitializeComponent();
        }

        private void BasicGaugeExample_Load(object sender, EventArgs e)
        {
            // Create gauge
            var gauge = new SpeedometerControl
            {
                MinValue = 0,
                MaxValue = 100,
                Value = 50,
                UnitLabel = "%",
                Size = new System.Drawing.Size(300, 300),
                Location = new System.Drawing.Point(10, 10)
            };

            this.Controls.Add(gauge);
        }
    }
}
```

---

## Example 2: RPMGaugeExample.cs

Realistic RPM gauge for vehicle dashboard.

```csharp
using System;
using System.Windows.Forms;
using System.Drawing;

namespace SpeedoMeter.Examples
{
    public partial class RPMGaugeExample : Form
    {
        private Timer rpmSimulationTimer;
        private SpeedometerControl rpmGauge;

        public RPMGaugeExample()
        {
            InitializeComponent();
            ConfigureRPMGauge();
        }

        private void ConfigureRPMGauge()
        {
            rpmGauge = new SpeedometerControl
            {
                // Core settings
                MinValue = 0,
                MaxValue = 8000,
                Value = 0,
                UnitLabel = "RPM",

                // Layout
                ScaleStartAngle = 225,
                ScaleSweepAngle = 270,
                GaugePadding = 15,

                // Appearance
                DialColor1 = Color.White,
                DialColor2 = Color.FromArgb(240, 240, 240),
                BezelColor1 = Color.White,
                BezelShadowColor = Color.FromArgb(200, 200, 200),

                // Zones (0-3000 Green, 3000-6000 Yellow, 6000+ Red)
                ZoneGreenEndValue = 3000,
                ZoneRedStartValue = 6000,
                ZoneGreenColor = Color.FromArgb(0, 200, 83),
                ZoneYellowColor = Color.FromArgb(255, 193, 7),
                ZoneRedColor = Color.FromArgb(244, 67, 54),

                // Needle
                NeedleStyle = NeedleStyle.ModernThin,
                NeedleColor1 = Color.Red,
                NeedleColor2 = Color.DarkRed,

                // Text
                ValueFontFamily = "Arial",
                TickLabelFontFamily = "Arial",

                // Sizing
                Size = new Size(450, 450),
                Location = new Point(20, 20)
            };

            this.Controls.Add(rpmGauge);

            // Start simulation
            rpmSimulationTimer = new Timer();
            rpmSimulationTimer.Interval = 50;
            rpmSimulationTimer.Tick += (s, e) => SimulateRPM();
            rpmSimulationTimer.Start();
        }

        private void SimulateRPM()
        {
            // Simulate engine acceleration and deceleration
            rpmGauge.Value += 30;

            if (rpmGauge.Value >= 7500)
                rpmGauge.Value = 0; // Reset
        }
    }
}
```

---

## Example 3: CPUMonitorExample.cs

Real-time system performance monitoring.

```csharp
using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;

namespace SpeedoMeter.Examples
{
    public partial class CPUMonitorExample : Form
    {
        private PerformanceCounter cpuCounter;
        private PerformanceCounter ramCounter;
        private Timer updateTimer;
        private SpeedometerControl cpuGauge;
        private SpeedometerControl ramGauge;

        public CPUMonitorExample()
        {
            InitializeComponent();
            InitializeCounters();
            CreateGauges();
        }

        private void InitializeCounters()
        {
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            ramCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");
        }

        private void CreateGauges()
        {
            // CPU Gauge
            cpuGauge = new SpeedometerControl
            {
                MinValue = 0,
                MaxValue = 100,
                UnitLabel = "CPU %",
                NeedleStyle = NeedleStyle.SciFiArrow,
                NeedleColor1 = Color.FromArgb(0, 200, 83),
                ZoneGreenEndValue = 50,
                ZoneRedStartValue = 80,
                Size = new Size(300, 300),
                Location = new Point(20, 20)
            };

            // RAM Gauge
            ramGauge = new SpeedometerControl
            {
                MinValue = 0,
                MaxValue = 100,
                UnitLabel = "RAM %",
                NeedleStyle = NeedleStyle.SciFiArrow,
                NeedleColor1 = Color.FromArgb(100, 150, 255),
                ZoneGreenEndValue = 50,
                ZoneRedStartValue = 80,
                Size = new Size(300, 300),
                Location = new Point(330, 20)
            };

            this.Controls.Add(cpuGauge);
            this.Controls.Add(ramGauge);

            // Start monitoring
            updateTimer = new Timer();
            updateTimer.Interval = 1000;
            updateTimer.Tick += (s, e) => UpdateMetrics();
            updateTimer.Start();
        }

        private void UpdateMetrics()
        {
            cpuGauge.Value = (float)cpuCounter.NextValue();
            ramGauge.Value = (float)ramCounter.NextValue();
        }

        protected override void Dispose(bool disposing)
        {
            updateTimer?.Dispose();
            cpuCounter?.Dispose();
            ramCounter?.Dispose();
            base.Dispose(disposing);
        }
    }
}
```

---

## Example 4: ThemesExample.cs

Demonstrates different color themes.

```csharp
using System.Drawing;
using System.Windows.Forms;

namespace SpeedoMeter.Examples
{
    public partial class ThemesExample : Form
    {
        public ThemesExample()
        {
            InitializeComponent();
            CreateThemGauges();
        }

        private void CreateThemGauges()
        {
            // Dark/Neon Theme
            var darkGauge = new SpeedometerControl
            {
                MinValue = 0,
                MaxValue = 100,
                Value = 65,
                UnitLabel = "Dark Theme",
                DialColor1 = Color.FromArgb(20, 20, 30),
                DialColor2 = Color.FromArgb(40, 40, 50),
                NeedleColor1 = Color.FromArgb(0, 255, 200),
                ValueTextColor = Color.White,
                TickLabelColor = Color.FromArgb(200, 200, 255),
                Size = new Size(250, 250),
                Location = new Point(20, 20)
            };

            // Vintage/Retro Theme
            var vintageGauge = new SpeedometerControl
            {
                MinValue = 0,
                MaxValue = 100,
                Value = 65,
                UnitLabel = "Vintage",
                DialColor1 = Color.FromArgb(240, 240, 220),
                DialColor2 = Color.FromArgb(200, 200, 180),
                NeedleStyle = NeedleStyle.VintageThinWithTail,
                NeedleColor1 = Color.FromArgb(140, 80, 50),
                TickLabelColor = Color.FromArgb(80, 60, 40),
                ValueTextColor = Color.FromArgb(100, 80, 60),
                Size = new Size(250, 250),
                Location = new Point(280, 20)
            };

            // Professional/Modern Theme
            var modernGauge = new SpeedometerControl
            {
                MinValue = 0,
                MaxValue = 100,
                Value = 65,
                UnitLabel = "Modern",
                DialColor1 = Color.White,
                DialColor2 = Color.FromArgb(240, 240, 245),
                NeedleStyle = NeedleStyle.ModernSleek,
                NeedleColor1 = Color.FromArgb(0, 128, 200),
                ZoneGreenEndValue = 40,
                ZoneRedStartValue = 70,
                Size = new Size(250, 250),
                Location = new Point(540, 20)
            };

            // Gaming/Sci-Fi Theme
            var gamingGauge = new SpeedometerControl
            {
                MinValue = 0,
                MaxValue = 100,
                Value = 65,
                UnitLabel = "Gaming",
                DialColor1 = Color.FromArgb(10, 10, 20),
                DialColor2 = Color.FromArgb(30, 30, 40),
                NeedleStyle = NeedleStyle.SciFiDataSpike,
                NeedleColor1 = Color.FromArgb(255, 100, 0),
                NeedleHighlightColor1 = Color.FromArgb(255, 255, 100),
                ValueTextColor = Color.FromArgb(255, 150, 0),
                Size = new Size(250, 250),
                Location = new Point(800, 20)
            };

            this.Controls.Add(darkGauge);
            this.Controls.Add(vintageGauge);
            this.Controls.Add(modernGauge);
            this.Controls.Add(gamingGauge);
        }
    }
}
```

---

## Example 5: AnimationExample.cs

Demonstrates smooth value animations.

```csharp
using System;
using System.Windows.Forms;
using System.Drawing;

namespace SpeedoMeter.Examples
{
    public partial class AnimationExample : Form
    {
        private SpeedometerControl gauge;
        private Timer animationTimer;
        private float targetValue = 0;
        private float currentValue = 0;
        private float animationSpeed = 2f;

        public AnimationExample()
        {
            InitializeComponent();
            CreateAnimatedGauge();
        }

        private void CreateAnimatedGauge()
        {
            gauge = new SpeedometerControl
            {
                MinValue = 0,
                MaxValue = 200,
                UnitLabel = "Speed",
                NeedleStyle = NeedleStyle.ClassicTapered,
                Size = new Size(400, 400),
                Location = new Point(50, 50)
            };

            this.Controls.Add(gauge);

            // Animation timer
            animationTimer = new Timer();
            animationTimer.Interval = 16; // ~60 FPS
            animationTimer.Tick += (s, e) => UpdateAnimation();
            animationTimer.Start();

            // Change target every 3 seconds
            var targetTimer = new Timer();
            targetTimer.Interval = 3000;
            targetTimer.Tick += (s, e) => SetRandomTarget();
            targetTimer.Start();
        }

        private void UpdateAnimation()
        {
            // Smooth interpolation
            currentValue = Lerp(currentValue, targetValue, 0.1f);
            gauge.Value = currentValue;
        }

        private void SetRandomTarget()
        {
            var random = new Random();
            targetValue = random.Next(0, 201);
        }

        private float Lerp(float from, float to, float t)
        {
            t = Math.Min(1, Math.Max(0, t));
            return from + (to - from) * t;
        }
    }
}
```

---

## Example 6: MultiGaugeLayoutExample.cs

Dashboard with multiple gauges.

```csharp
using System.Drawing;
using System.Windows.Forms;

namespace SpeedoMeter.Examples
{
    public partial class MultiGaugeLayoutExample : Form
    {
        public MultiGaugeLayoutExample()
        {
            InitializeComponent();
            CreateDashboard();
        }

        private void CreateDashboard()
        {
            this.Text = "Vehicle Dashboard";
            this.Size = new Size(900, 550);

            // Speed Gauge (center, larger)
            var speedGauge = new SpeedometerControl
            {
                MinValue = 0,
                MaxValue = 300,
                Value = 120,
                UnitLabel = "km/h",
                Size = new Size(350, 350),
                Location = new Point(275, 50)
            };

            // RPM Gauge (top-left)
            var rpmGauge = new SpeedometerControl
            {
                MinValue = 0,
                MaxValue = 8000,
                Value = 3500,
                UnitLabel = "RPM",
                NeedleStyle = NeedleStyle.ModernIndicator,
                Size = new Size(200, 200),
                Location = new Point(20, 50)
            };

            // Fuel Gauge (top-right)
            var fuelGauge = new SpeedometerControl
            {
                MinValue = 0,
                MaxValue = 100,
                Value = 75,
                UnitLabel = "Fuel",
                NeedleStyle = NeedleStyle.SimplePin,
                Size = new Size(200, 200),
                Location = new Point(680, 50)
            };

            // Temperature Gauge (bottom-left)
            var tempGauge = new SpeedometerControl
            {
                MinValue = 0,
                MaxValue = 120,
                Value = 85,
                UnitLabel = "°C",
                NeedleStyle = NeedleStyle.ModernSleek,
                ZoneGreenEndValue = 90,
                ZoneRedStartValue = 110,
                Size = new Size(200, 200),
                Location = new Point(20, 280)
            };

            // Tachometer Gauge (bottom-right)
            var tacoGauge = new SpeedometerControl
            {
                MinValue = 0,
                MaxValue = 10000,
                Value = 4200,
                UnitLabel = "Tach",
                NeedleStyle = NeedleStyle.Arrowhead,
                Size = new Size(200, 200),
                Location = new Point(680, 280)
            };

            this.Controls.Add(speedGauge);
            this.Controls.Add(rpmGauge);
            this.Controls.Add(fuelGauge);
            this.Controls.Add(tempGauge);
            this.Controls.Add(tacoGauge);
        }
    }
}
```

---

## Tips & Best Practices

1. **Always dispose timers** when closing forms
2. **Use appropriate needle styles** for context
3. **Test with extreme values** (min, max, out of range)
4. **Consider color-blind users** in zone colors
5. **Profile performance** with many controls
6. **Use zones** to communicate status
7. **Scale controls** responsively with form resize

---

**Happy Exploring!** 🎯
