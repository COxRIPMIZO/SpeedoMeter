using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SpeedoMeter
{
    public partial class SpeedometerControl_2 : UserControl
    {
        #region --- Backing Fields for All Properties ---
        private float _value = 15f;
        private float _minValue = 0f;
        private float _maxValue = 400f;
        private string _unitLabel = "m/min";
        private float _scaleStartAngle = 135f;
        private float _scaleSweepAngle = 270f;
        private int _gaugePadding = 8;
        private float _dialPadding = 4f;
        private float _tickZonePadding = 2f;
        private float _tickLabelSpacing = 4f;
        private PointF _valuePosition = new PointF(0.5f, 0.85f);
        private float _bezelWidthMultiplier = 0.08f;
        private float _zoneGreenEndValue = 100f;
        private float _zoneRedStartValue = 250f;
        private Color _zoneGreenColor = Color.FromArgb(0, 200, 83);
        private Color _zoneYellowColor = Color.FromArgb(255, 193, 7);
        private Color _zoneRedColor = Color.FromArgb(244, 67, 54);
        private float _zoneThicknessMultiplier = 0.04f;
        private float _zoneMinThickness = 2f;
        private int _majorTickStep = 0;
        private int _desiredTickLabelCount = 8;
        private int _minorTicksPerMajor = 5;
        private float _majorTickLengthMultiplier = 0.17f;
        private float _minorTickLengthMultiplier = 0.11f;
        private float _majorTickWidth = 2f;
        private float _minorTickWidth = 1f;
        private string _tickLabelFontFamily = "Segoe UI";
        private float _tickLabelFontSizeMultiplier = 0.04f;
        private float _tickLabelMinFontSize = 10f;
        private Color _tickLabelColor = Color.FromArgb(220, 60, 60, 60);
        private Color _majorTickColor = Color.FromArgb(220, 60, 60, 60);
        private Color _minorTickColor = Color.FromArgb(160, 60, 60, 60);
        private string _valueFontFamily = "Segoe UI";
        private float _valueFontSizeMultiplier = 0.1f;
        private float _unitFontSizeScale = 0.5f;
        private float _valueUnitVerticalSpacing = -22f;
        private float _valueMinFontSize = 18f;
        private float _unitMinFontSize = 8f;
        private Color _valueTextColor = Color.FromArgb(220, 40, 40, 40);
        private Color _unitTextColor = Color.FromArgb(180, 80, 80, 80);
        private int _needleTransparency = 180;
        private float _needleTipMultiplier = 0.75f;
        private float _needleTailMultiplier = 0.20f;
        private float _needleTailSplitDistanceMultiplier = 0.24f;
        private float _needleShoulderDistanceMultiplier = 0.08f;
        private float _needleShoulderWidthMultiplier = 0.055f;
        private float _needleTailWidthMultiplier = 0.03f;
        private float _needleHighlightWidth = 2.0f;
        private float _needleShadowOffsetMultiplier = 0.02f;
        private Color _needleColor1 = Color.FromArgb(255, 80, 80);
        private Color _needleColor2 = Color.FromArgb(180, 30, 30);
        private Color _needleHighlightColor1 = Color.FromArgb(200, 255, 255, 255);
        private Color _needleHighlightColor2 = Color.FromArgb(0, 255, 255, 255);
        private Color _needleShadowColor = Color.FromArgb(72, 0, 0, 0);
        private float _hubSizeMultiplier = 0.12f;
        private float _hubMinSize = 12f;
        private Color _hubColor1 = Color.FromArgb(200, 200, 200);
        private Color _hubColor2 = Color.FromArgb(100, 100, 100);
        private Color _hubShadowColor = Color.FromArgb(50, 0, 0, 0);
        private Color _hubHighlightColor = Color.FromArgb(150, 255, 255, 255);
        private Color _faceColor1 = Color.White;
        private Color _faceColor2 = Color.FromArgb(248, 248, 248);
        private Color _outerBorderColor = Color.FromArgb(120, 80, 80, 80);
        private float _outerBorderWidth = 1.5f;
        private Color _outerShadowColor = Color.FromArgb(50, 0, 0, 0);
        private Color _innerShadowColor = Color.FromArgb(100, 0, 0, 0);
        private Color _textShadowColor = Color.FromArgb(30, 0, 0, 0);
        private Color _glassEffectCenterColor = Color.FromArgb(120, 255, 255, 255);
        private Color _glassEffectOuterColor = Color.FromArgb(0, 255, 255, 255);
        private RectangleF _glassEffectRect = new RectangleF(0.1f, 0.05f, 0.8f, 0.4f);
        #endregion

        #region --- Fully Exposed Properties with Invalidate() ---

        #region Core Gauge
        [Category("1. Gauge Core")] public float Value { get => _value; set { _value = Clamp(value, _minValue, _maxValue); Invalidate(); } }
        [Category("1. Gauge Core")] public float MinValue { get => _minValue; set { _minValue = value; if (_maxValue < _minValue) _maxValue = _minValue; _value = Clamp(_value, _minValue, _maxValue); Invalidate(); } }
        [Category("1. Gauge Core")] public float MaxValue { get => _maxValue; set { _maxValue = Math.Max(value, _minValue); _value = Clamp(_value, _minValue, _maxValue); Invalidate(); } }
        [Category("1. Gauge Core")] public string UnitLabel { get => _unitLabel; set { _unitLabel = value ?? ""; Invalidate(); } }
        #endregion

        #region Layout & Sizing
        [Category("2. Gauge Layout")][Description("The start angle of the scale in degrees.")] public float ScaleStartAngle { get => _scaleStartAngle; set { _scaleStartAngle = value; Invalidate(); } }
        [Category("2. Gauge Layout")][Description("The sweep angle of the scale in degrees.")] public float ScaleSweepAngle { get => _scaleSweepAngle; set { _scaleSweepAngle = value; Invalidate(); } }
        [Category("2. Gauge Layout")][Description("The overall padding for the control within its container.")] public int GaugePadding { get => _gaugePadding; set { _gaugePadding = value; Invalidate(); } }
        [Category("2. Gauge Layout")][Description("The padding in pixels between the silver bezel and the colored zones.")] public float DialPadding { get => _dialPadding; set { _dialPadding = value; Invalidate(); } }
        [Category("2. Gauge Layout")][Description("The padding in pixels between the colored zones and the start of the tick marks.")] public float TickZonePadding { get => _tickZonePadding; set { _tickZonePadding = value; Invalidate(); } }
        [Category("2. Gauge Layout")][Description("The spacing in pixels between the tick marks and their numeric labels.")] public float TickLabelSpacing { get => _tickLabelSpacing; set { _tickLabelSpacing = value; Invalidate(); } }
        [Category("2. Gauge Layout")][Description("Controls the anchor point for the value and unit labels. (0.5, 0.85) is the lower bottom.")] public PointF ValuePosition { get => _valuePosition; set { _valuePosition = value; Invalidate(); } }
        [Category("2. Gauge Layout")][Description("The multiplier for the bezel's width, relative to the gauge radius.")] public float BezelWidthMultiplier { get => _bezelWidthMultiplier; set { _bezelWidthMultiplier = value; Invalidate(); } }
        #endregion

        #region Zones
        [Category("3. Gauge Zones")] public float ZoneGreenEndValue { get => _zoneGreenEndValue; set { _zoneGreenEndValue = value; Invalidate(); } }
        [Category("3. Gauge Zones")] public float ZoneRedStartValue { get => _zoneRedStartValue; set { _zoneRedStartValue = value; Invalidate(); } }
        [Category("3. Gauge Zones")] public Color ZoneGreenColor { get => _zoneGreenColor; set { _zoneGreenColor = value; Invalidate(); } }
        [Category("3. Gauge Zones")] public Color ZoneYellowColor { get => _zoneYellowColor; set { _zoneYellowColor = value; Invalidate(); } }
        [Category("3. Gauge Zones")] public Color ZoneRedColor { get => _zoneRedColor; set { _zoneRedColor = value; Invalidate(); } }
        [Category("3. Gauge Zones")][Description("The thickness of the colored zone arcs, as a multiplier of the gauge radius.")] public float ZoneThicknessMultiplier { get => _zoneThicknessMultiplier; set { _zoneThicknessMultiplier = value; Invalidate(); } }
        [Category("3. Gauge Zones")][Description("The minimum thickness of the zones in pixels.")] public float ZoneMinThickness { get => _zoneMinThickness; set { _zoneMinThickness = value; Invalidate(); } }
        #endregion

        #region Ticks & Labels
        [Category("4. Gauge Ticks")][Description("Overrides the automatic calculation for the major tick step. Set to 0 for auto.")] public int MajorTickStep { get => _majorTickStep; set { _majorTickStep = value; Invalidate(); } }
        [Category("4. Gauge Ticks")][Description("The desired number of labels to show when MajorTickStep is set to auto (0).")] public int DesiredTickLabelCount { get => _desiredTickLabelCount; set { _desiredTickLabelCount = value; Invalidate(); } }
        [Category("4. Gauge Ticks")][Description("The number of minor ticks to show between major ticks.")] public int MinorTicksPerMajor { get => _minorTicksPerMajor; set { _minorTicksPerMajor = value; Invalidate(); } }
        [Category("4. Gauge Ticks")][Description("The length of major ticks, as a multiplier of the gauge radius.")] public float MajorTickLengthMultiplier { get => _majorTickLengthMultiplier; set { _majorTickLengthMultiplier = value; Invalidate(); } }
        [Category("4. Gauge Ticks")][Description("The length of minor ticks, as a multiplier of the gauge radius.")] public float MinorTickLengthMultiplier { get => _minorTickLengthMultiplier; set { _minorTickLengthMultiplier = value; Invalidate(); } }
        [Category("4. Gauge Ticks")][Description("The width of major ticks in pixels.")] public float MajorTickWidth { get => _majorTickWidth; set { _majorTickWidth = value; Invalidate(); } }
        [Category("4. Gauge Ticks")][Description("The width of minor ticks in pixels.")] public float MinorTickWidth { get => _minorTickWidth; set { _minorTickWidth = value; Invalidate(); } }
        [Category("4. Gauge Ticks")] public string TickLabelFontFamily { get => _tickLabelFontFamily; set { _tickLabelFontFamily = value; Invalidate(); } }
        [Category("4. Gauge Ticks")][Description("A multiplier for the font size of the tick mark labels.")] public float TickLabelFontSizeMultiplier { get => _tickLabelFontSizeMultiplier; set { _tickLabelFontSizeMultiplier = value; Invalidate(); } }
        [Category("4. Gauge Ticks")][Description("Minimum font size for tick labels in points.")] public float TickLabelMinFontSize { get => _tickLabelMinFontSize; set { _tickLabelMinFontSize = value; Invalidate(); } }
        [Category("4. Gauge Ticks")] public Color TickLabelColor { get => _tickLabelColor; set { _tickLabelColor = value; Invalidate(); } }
        [Category("4. Gauge Ticks")] public Color MajorTickColor { get => _majorTickColor; set { _majorTickColor = value; Invalidate(); } }
        [Category("4. Gauge Ticks")] public Color MinorTickColor { get => _minorTickColor; set { _minorTickColor = value; Invalidate(); } }
        #endregion

        #region Value Display
        [Category("5. Gauge Value Display")] public string ValueFontFamily { get => _valueFontFamily; set { _valueFontFamily = value; Invalidate(); } }
        [Category("5. Gauge Value Display")][Description("A multiplier for the font size of the main value text.")] public float ValueFontSizeMultiplier { get => _valueFontSizeMultiplier; set { _valueFontSizeMultiplier = value; Invalidate(); } }
        [Category("5. Gauge Value Display")][Description("A multiplier for the font size of the unit text, relative to the value text size.")] public float UnitFontSizeScale { get => _unitFontSizeScale; set { _unitFontSizeScale = value; Invalidate(); } }
        [Category("5. Gauge Value Display")][Description("The vertical spacing in pixels between the value and unit text.")] public float ValueUnitVerticalSpacing { get => _valueUnitVerticalSpacing; set { _valueUnitVerticalSpacing = value; Invalidate(); } }
        [Category("5. Gauge Value Display")][Description("Minimum font size for the main value in points.")] public float ValueMinFontSize { get => _valueMinFontSize; set { _valueMinFontSize = value; Invalidate(); } }
        [Category("5. Gauge Value Display")][Description("Minimum font size for the unit text in points.")] public float UnitMinFontSize { get => _unitMinFontSize; set { _unitMinFontSize = value; Invalidate(); } }
        [Category("5. Gauge Value Display")] public Color ValueTextColor { get => _valueTextColor; set { _valueTextColor = value; Invalidate(); } }
        [Category("5. Gauge Value Display")] public Color UnitTextColor { get => _unitTextColor; set { _unitTextColor = value; Invalidate(); } }
        #endregion

        #region Needle Shape & Color
        [Category("6. Needle")][Description("The transparency of the needle, from 0 (transparent) to 255 (opaque).")] public int NeedleTransparency { get => _needleTransparency; set { _needleTransparency = value; Invalidate(); } }
        [Category("6. Needle")][Description("The distance of the needle tip from the center, as a multiplier of the radius.")] public float NeedleTipMultiplier { get => _needleTipMultiplier; set { _needleTipMultiplier = value; Invalidate(); } }
        [Category("6. Needle")][Description("The distance of the needle tail from the center, as a multiplier of the radius.")] public float NeedleTailMultiplier { get => _needleTailMultiplier; set { _needleTailMultiplier = value; Invalidate(); } }
        [Category("6. Needle")][Description("The distance of the forked tail section from the center, as a multiplier of the radius.")] public float NeedleTailSplitDistanceMultiplier { get => _needleTailSplitDistanceMultiplier; set { _needleTailSplitDistanceMultiplier = value; Invalidate(); } }
        [Category("6. Needle")][Description("The distance of the needle's 'shoulders' from the center, as a multiplier of the radius.")] public float NeedleShoulderDistanceMultiplier { get => _needleShoulderDistanceMultiplier; set { _needleShoulderDistanceMultiplier = value; Invalidate(); } }
        [Category("6. Needle")][Description("The width of the needle at its shoulders, as a multiplier of the radius.")] public float NeedleShoulderWidthMultiplier { get => _needleShoulderWidthMultiplier; set { _needleShoulderWidthMultiplier = value; Invalidate(); } }
        [Category("6. Needle")][Description("The width of the needle at its tail, as a multiplier of the radius.")] public float NeedleTailWidthMultiplier { get => _needleTailWidthMultiplier; set { _needleTailWidthMultiplier = value; Invalidate(); } }
        [Category("6. Needle")][Description("The width of the central highlight on the needle.")] public float NeedleHighlightWidth { get => _needleHighlightWidth; set { _needleHighlightWidth = value; Invalidate(); } }
        [Category("6. Needle")][Description("The offset of the needle's shadow, as a multiplier of the radius.")] public float NeedleShadowOffsetMultiplier { get => _needleShadowOffsetMultiplier; set { _needleShadowOffsetMultiplier = value; Invalidate(); } }
        [Category("6. Needle")][Description("The main color of the needle.")] public Color NeedleColor1 { get => _needleColor1; set { _needleColor1 = value; Invalidate(); } }
        [Category("6. Needle")][Description("The shaded color of the needle.")] public Color NeedleColor2 { get => _needleColor2; set { _needleColor2 = value; Invalidate(); } }
        [Category("6. Needle")][Description("The start color of the central highlight on the needle.")] public Color NeedleHighlightColor1 { get => _needleHighlightColor1; set { _needleHighlightColor1 = value; Invalidate(); } }
        [Category("6. Needle")][Description("The end color of the central highlight on the needle.")] public Color NeedleHighlightColor2 { get => _needleHighlightColor2; set { _needleHighlightColor2 = value; Invalidate(); } }
        [Category("6. Needle")][Description("The color of the needle's drop shadow.")] public Color NeedleShadowColor { get => _needleShadowColor; set { _needleShadowColor = value; Invalidate(); } }
        #endregion

        #region Hub
        [Category("7. Hub")][Description("The size of the central hub, as a multiplier of the radius.")] public float HubSizeMultiplier { get => _hubSizeMultiplier; set { _hubSizeMultiplier = value; Invalidate(); } }
        [Category("7. Hub")][Description("Minimum size of the hub in pixels.")] public float HubMinSize { get => _hubMinSize; set { _hubMinSize = value; Invalidate(); } }
        [Category("7. Hub")][Description("The start color of the hub's gradient.")] public Color HubColor1 { get => _hubColor1; set { _hubColor1 = value; Invalidate(); } }
        [Category("7. Hub")][Description("The end color of the hub's gradient.")] public Color HubColor2 { get => _hubColor2; set { _hubColor2 = value; Invalidate(); } }
        [Category("7. Hub")][Description("The color of the hub's outer shadow/border.")] public Color HubShadowColor { get => _hubShadowColor; set { _hubShadowColor = value; Invalidate(); } }
        [Category("7. Hub")][Description("The color of the hub's inner highlight.")] public Color HubHighlightColor { get => _hubHighlightColor; set { _hubHighlightColor = value; Invalidate(); } }
        #endregion

        #region Advanced Colors & Effects
        [Category("8. Advanced Effects")][Description("The center color of the dial face gradient.")] public Color FaceColor1 { get => _faceColor1; set { _faceColor1 = value; Invalidate(); } }
        [Category("8. Advanced Effects")][Description("The outer edge color of the dial face gradient.")] public Color FaceColor2 { get => _faceColor2; set { _faceColor2 = value; Invalidate(); } }
        [Category("8. Advanced Effects")][Description("The color of the thin outer border.")] public Color OuterBorderColor { get => _outerBorderColor; set { _outerBorderColor = value; Invalidate(); } }
        [Category("8. Advanced Effects")][Description("The width of the thin outer border.")] public float OuterBorderWidth { get => _outerBorderWidth; set { _outerBorderWidth = value; Invalidate(); } }
        [Category("8. Advanced Effects")][Description("The center color of the outer drop shadow.")] public Color OuterShadowColor { get => _outerShadowColor; set { _outerShadowColor = value; Invalidate(); } }
        [Category("8. Advanced Effects")][Description("The center color of the shadow inside the bezel.")] public Color InnerShadowColor { get => _innerShadowColor; set { _innerShadowColor = value; Invalidate(); } }
        [Category("8. Advanced Effects")][Description("The color of the drop shadow for ticks and labels.")] public Color TextShadowColor { get => _textShadowColor; set { _textShadowColor = value; Invalidate(); } }
        [Category("8. Advanced Effects")][Description("The center color of the glass highlight effect.")] public Color GlassEffectCenterColor { get => _glassEffectCenterColor; set { _glassEffectCenterColor = value; Invalidate(); } }
        [Category("8. Advanced Effects")][Description("The outer color of the glass highlight effect.")] public Color GlassEffectOuterColor { get => _glassEffectOuterColor; set { _glassEffectOuterColor = value; Invalidate(); } }
        [Category("8. Advanced Effects")][Description("The bounding box of the glass effect, as multipliers of the gauge width/height. (X, Y, Width, Height)")] public RectangleF GlassEffectRect { get => _glassEffectRect; set { _glassEffectRect = value; Invalidate(); } }
        #endregion

        #endregion

        public SpeedometerControl_2()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            Size = new Size(340, 340);
            BackColor = Color.White;
        }

        #region --- Drawing Methods ---
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.Clear(BackColor);

            var bounds = ClientRectangle;
            int size = Math.Min(bounds.Width, bounds.Height) - GaugePadding * 2;
            if (size <= 8) return;
            int left = bounds.Left + (bounds.Width - size) / 2;
            int top = bounds.Top + (bounds.Height - size) / 2;
            var dialRect = new Rectangle(left, top, size, size);

            DrawOuterShadow(g, dialRect);
            DrawFace(g, dialRect);
            DrawBezel(g, dialRect);
            DrawInnerBezelShadow(g, dialRect);
            DrawZones(g, dialRect);
            DrawTicks(g, dialRect);
            DrawNeedle(g, dialRect);
            DrawValueLabel(g, dialRect);
            DrawThinBorder(g, dialRect);
            DrawGlassEffect(g, dialRect);
        }

        private void DrawOuterShadow(Graphics g, Rectangle rect)
        {
            var shadowRect = RectangleF.Inflate(rect, rect.Width * 0.03f, rect.Height * 0.03f);
            using (var p = new GraphicsPath())
            {
                p.AddEllipse(shadowRect);
                using (var pb = new PathGradientBrush(p))
                {
                    pb.CenterColor = OuterShadowColor;
                    pb.SurroundColors = new[] { Color.FromArgb(0, OuterShadowColor) };
                    pb.FocusScales = new PointF(0.85f, 0.85f);
                    g.FillPath(pb, p);
                }
            }
        }

        private void DrawFace(Graphics g, Rectangle rect)
        {
            using (var facePath = new GraphicsPath())
            {
                facePath.AddEllipse(rect);
                using (var faceBrush = new PathGradientBrush(facePath))
                {
                    faceBrush.CenterColor = FaceColor1;
                    faceBrush.SurroundColors = new[] { FaceColor2 };
                    faceBrush.FocusScales = new PointF(0.7f, 0.7f);
                    g.FillPath(faceBrush, facePath);
                }
            }
        }

        private void DrawBezel(Graphics g, Rectangle rect)
        {
            float radius = rect.Width * 0.5f;
            float rimWidth = Math.Max(4f, radius * BezelWidthMultiplier);
            var rimRect = RectangleF.Inflate(rect, -rimWidth / 2f, -rimWidth / 2f);

            using (var path = new GraphicsPath())
            {
                path.AddEllipse(rimRect);
                using (var brush = new LinearGradientBrush(rect, Color.FromArgb(224, 224, 224), Color.FromArgb(200, 200, 200), 45f)) { g.FillPath(brush, path); }
                using (var brush = new LinearGradientBrush(rect, Color.FromArgb(250, 250, 250), Color.Transparent, 225f)) { g.FillPath(brush, path); }
                using (var brush = new LinearGradientBrush(rect, Color.Transparent, Color.FromArgb(190, 190, 190), 45f)) { g.FillPath(brush, path); }
            }
        }

        private void DrawInnerBezelShadow(Graphics g, Rectangle rect)
        {
            float radius = rect.Width * 0.5f;
            float rimWidth = Math.Max(4f, radius * BezelWidthMultiplier);
            var shadowRect = RectangleF.Inflate(rect, -rimWidth * 0.9f, -rimWidth * 0.9f);
            using (var path = new GraphicsPath())
            {
                path.AddEllipse(rect);
                path.AddEllipse(shadowRect);
                using (var shadowBrush = new PathGradientBrush(path))
                {
                    shadowBrush.CenterColor = InnerShadowColor;
                    shadowBrush.SurroundColors = new[] { Color.FromArgb(0, InnerShadowColor) };
                    g.FillPath(shadowBrush, path);
                }
            }
        }

        private void DrawGlassEffect(Graphics g, Rectangle rect)
        {
            var effect = GlassEffectRect;
            var highlightRect = new RectangleF(rect.Left + rect.Width * effect.X, rect.Top + rect.Height * effect.Y, rect.Width * effect.Width, rect.Height * effect.Height);
            using (var path = new GraphicsPath())
            {
                path.AddEllipse(highlightRect);
                using (var brush = new PathGradientBrush(path))
                {
                    brush.CenterColor = GlassEffectCenterColor;
                    brush.SurroundColors = new[] { GlassEffectOuterColor };
                    brush.FocusScales = new PointF(0.5f, 0.5f);
                    g.FillPath(brush, path);
                }
            }
        }

        private void DrawZones(Graphics g, Rectangle rect)
        {
            float greenSpan = SpanForRange(MinValue, ZoneGreenEndValue);
            float yellowSpan = SpanForRange(ZoneGreenEndValue, ZoneRedStartValue);
            float redSpan = SpanForRange(ZoneRedStartValue, MaxValue);
            float radius = rect.Width * 0.5f;
            float bezelWidth = radius * BezelWidthMultiplier;
            float zoneThickness = Math.Max(ZoneMinThickness, radius * ZoneThicknessMultiplier);
            float totalInset = bezelWidth + DialPadding + (zoneThickness / 2f);
            var zoneRect = RectangleF.Inflate(rect, -totalInset, -totalInset);

            using (var pG = new Pen(ZoneGreenColor, zoneThickness))
            using (var pY = new Pen(ZoneYellowColor, zoneThickness))
            using (var pR = new Pen(ZoneRedColor, zoneThickness))
            {
                pG.EndCap = LineCap.Round; pY.EndCap = LineCap.Round; pR.EndCap = LineCap.Round;
                if (greenSpan > 0) g.DrawArc(pG, zoneRect, ScaleStartAngle, greenSpan);
                if (yellowSpan > 0) g.DrawArc(pY, zoneRect, ScaleStartAngle + greenSpan, yellowSpan);
                if (redSpan > 0) g.DrawArc(pR, zoneRect, ScaleStartAngle + greenSpan + yellowSpan, redSpan);
            }
        }

        private void DrawTicks(Graphics g, Rectangle rect)
        {
            float radius = rect.Width * 0.5f;
            var center = new PointF(rect.Left + radius, rect.Top + radius);
            float valueRange = Math.Max(1f, MaxValue - MinValue);
            int majorStep = (MajorTickStep > 0) ? MajorTickStep : NiceNumber(valueRange / Math.Max(1, DesiredTickLabelCount));
            int minorStep = Math.Max(1, majorStep / Math.Max(1, MinorTicksPerMajor));
            float bezelWidth = radius * BezelWidthMultiplier;
            float zoneThickness = Math.Max(ZoneMinThickness, radius * ZoneThicknessMultiplier);
            float outerTickInset = bezelWidth + DialPadding + zoneThickness + TickZonePadding;
            float majorTickLength = radius * MajorTickLengthMultiplier;
            float minorTickLength = radius * MinorTickLengthMultiplier;
            float majorInnerTickInset = outerTickInset + majorTickLength;
            float minorInnerTickInset = outerTickInset + minorTickLength;
            float fontSize = Math.Max(TickLabelMinFontSize, radius * TickLabelFontSizeMultiplier);

            using (var tickFont = new Font(TickLabelFontFamily, fontSize, FontStyle.Bold))
            using (var tickBrush = new SolidBrush(TickLabelColor))
            using (var shadowBrush = new SolidBrush(TextShadowColor))
            using (var majorPen = new Pen(MajorTickColor, MajorTickWidth))
            using (var minorPen = new Pen(MinorTickColor, MinorTickWidth))
            {
                int startValue = (int)(Math.Ceiling(MinValue / minorStep) * minorStep);
                int endValue = (int)(Math.Floor(MaxValue / minorStep) * minorStep);
                for (int v = startValue; v <= endValue; v += minorStep)
                {
                    bool isMajor = (v % majorStep == 0);
                    float innerInset = isMajor ? majorInnerTickInset : minorInnerTickInset;
                    var p1 = PointOnCircle(center, radius - outerTickInset, v);
                    var p2 = PointOnCircle(center, radius - innerInset, v);
                    g.DrawLine(isMajor ? majorPen : minorPen, p1, p2);
                }
                int labelStartValue = (int)(Math.Ceiling(MinValue / majorStep) * majorStep);
                for (int v = labelStartValue; v <= MaxValue; v += majorStep)
                {
                    DrawLabelAtValue(g, center, radius, v, majorInnerTickInset, tickFont, tickBrush, shadowBrush);
                }
                DrawLabelAtValue(g, center, radius, (int)MinValue, majorInnerTickInset, tickFont, tickBrush, shadowBrush);
                DrawLabelAtValue(g, center, radius, (int)MaxValue, majorInnerTickInset, tickFont, tickBrush, shadowBrush);
            }
        }

        private void DrawLabelAtValue(Graphics g, PointF center, float radius, int value, float tickEndInset, Font font, Brush brush, Brush shadowBrush)
        {
            string label = value.ToString();
            SizeF size = g.MeasureString(label, font);
            float labelCenterInset = tickEndInset + TickLabelSpacing + (size.Height / 2f);
            var pos = PointOnCircle(center, radius - labelCenterInset, value);
            var rectPos = new PointF(pos.X - size.Width / 2f, pos.Y - size.Height / 2f);
            g.DrawString(label, font, shadowBrush, rectPos.X + 1, rectPos.Y + 1);
            g.DrawString(label, font, brush, rectPos);
        }

        private void DrawNeedle(Graphics g, Rectangle rect)
        {
            float radius = rect.Width * 0.5f;
            var center = new PointF(rect.Left + radius, rect.Top + radius);
            float angle = MapValueToAngle(Value);
            double rad = DegToRad(angle);
            var dir = new PointF((float)Math.Cos(rad), (float)Math.Sin(rad));
            var perp = new PointF(-dir.Y, dir.X);
            var tip = new PointF(center.X + dir.X * (radius * NeedleTipMultiplier), center.Y + dir.Y * (radius * NeedleTipMultiplier));
            var shoulderLeft = new PointF(center.X + dir.X * (radius * NeedleShoulderDistanceMultiplier) - perp.X * (radius * NeedleShoulderWidthMultiplier), center.Y + dir.Y * (radius * NeedleShoulderDistanceMultiplier) - perp.Y * (radius * NeedleShoulderWidthMultiplier));
            var shoulderRight = new PointF(center.X + dir.X * (radius * NeedleShoulderDistanceMultiplier) + perp.X * (radius * NeedleShoulderWidthMultiplier), center.Y + dir.Y * (radius * NeedleShoulderDistanceMultiplier) + perp.Y * (radius * NeedleShoulderWidthMultiplier));
            var tailLeft = new PointF(center.X - dir.X * (radius * NeedleTailSplitDistanceMultiplier) - perp.X * (radius * NeedleTailWidthMultiplier), center.Y - dir.Y * (radius * NeedleTailSplitDistanceMultiplier) - perp.Y * (radius * NeedleTailWidthMultiplier));
            var tailRight = new PointF(center.X - dir.X * (radius * NeedleTailSplitDistanceMultiplier) + perp.X * (radius * NeedleTailWidthMultiplier), center.Y - dir.Y * (radius * NeedleTailSplitDistanceMultiplier) + perp.Y * (radius * NeedleTailWidthMultiplier));
            var tailCenter = new PointF(center.X - dir.X * (radius * NeedleTailMultiplier), center.Y - dir.Y * (radius * NeedleTailMultiplier));
            var needlePoints = new[] { tip, shoulderRight, tailRight, tailCenter, tailLeft, shoulderLeft };

            using (var path = new GraphicsPath())
            {
                path.AddPolygon(needlePoints);
                using (var shadowPath = (GraphicsPath)path.Clone())
                {
                    var m = new Matrix();
                    m.Translate(radius * NeedleShadowOffsetMultiplier, radius * NeedleShadowOffsetMultiplier);
                    shadowPath.Transform(m);
                    using (var shadowBrush = new SolidBrush(NeedleShadowColor)) { g.FillPath(shadowBrush, shadowPath); }
                }
                using (var brush = new LinearGradientBrush(path.GetBounds(), Color.FromArgb(NeedleTransparency, NeedleColor1), Color.FromArgb(NeedleTransparency, NeedleColor2), angle + 90)) { g.FillPath(brush, path); }
                using (var highlightPath = new GraphicsPath())
                {
                    highlightPath.AddLine(shoulderLeft, tip);
                    highlightPath.AddLine(tip, shoulderRight);
                    using (var lgb = new LinearGradientBrush(path.GetBounds(), NeedleHighlightColor1, NeedleHighlightColor2, angle))
                    using (var pen = new Pen(lgb, NeedleHighlightWidth)) { g.DrawPath(pen, highlightPath); }
                }
            }
            float hubRadius = Math.Max(HubMinSize, radius * HubSizeMultiplier);
            var hubRect = new RectangleF(center.X - hubRadius, center.Y - hubRadius, hubRadius * 2, hubRadius * 2);
            using (var hubPath = new GraphicsPath())
            {
                hubPath.AddEllipse(hubRect);
                using (var hubBrush = new LinearGradientBrush(hubRect, HubColor1, HubColor2, 45f)) { g.FillPath(hubBrush, hubPath); }
                using (var pen = new Pen(HubHighlightColor, 1f)) { g.DrawEllipse(pen, RectangleF.Inflate(hubRect, -2, -2)); }
                using (var pen = new Pen(HubShadowColor, 2f)) { g.DrawEllipse(pen, hubRect); }
            }
        }

        private void DrawValueLabel(Graphics g, Rectangle rect)
        {
            string valueText = ((int)_value).ToString();
            string unitText = _unitLabel;
            float radius = rect.Width * 0.5f;
            var blockCenter = new PointF(rect.Left + rect.Width * ValuePosition.X, rect.Top + rect.Height * ValuePosition.Y);
            float fontSizeValue = Math.Max(ValueMinFontSize, radius * ValueFontSizeMultiplier);
            float fontSizeUnit = Math.Max(UnitMinFontSize, fontSizeValue * UnitFontSizeScale);

            using (var valueFont = new Font(ValueFontFamily, fontSizeValue, FontStyle.Bold))
            using (var unitFont = new Font(ValueFontFamily, fontSizeUnit, FontStyle.Regular))
            using (var valueBrush = new SolidBrush(ValueTextColor))
            using (var unitBrush = new SolidBrush(UnitTextColor))
            {
                var valueSize = g.MeasureString(valueText, valueFont);
                var unitSize = g.MeasureString(unitText, unitFont);
                float totalBlockHeight = valueSize.Height + unitSize.Height + ValueUnitVerticalSpacing;
                float valueTextY = blockCenter.Y - (totalBlockHeight / 2f);
                float unitTextY = valueTextY + valueSize.Height + ValueUnitVerticalSpacing;
                g.DrawString(valueText, valueFont, valueBrush, blockCenter.X - valueSize.Width / 2f, valueTextY);
                g.DrawString(unitText, unitFont, unitBrush, blockCenter.X - unitSize.Width / 2f, unitTextY);
            }
        }

        private void DrawThinBorder(Graphics g, Rectangle rect)
        {
            using (var pen = new Pen(OuterBorderColor, OuterBorderWidth))
            {
                g.DrawEllipse(pen, rect);
            }
        }
        #endregion

        #region --- Helper Methods ---
        private float MapValueToAngle(float v)
        {
            float range = MaxValue - MinValue;
            if (range <= 0) return ScaleStartAngle;
            float t = (Clamp(v, MinValue, MaxValue) - MinValue) / range;
            return ScaleStartAngle + t * ScaleSweepAngle;
        }

        private PointF PointOnCircle(PointF center, float radius, float value)
        {
            float angle = MapValueToAngle(value);
            double rad = DegToRad(angle);
            return new PointF(center.X + (float)(Math.Cos(rad) * radius), center.Y + (float)(Math.Sin(rad) * radius));
        }

        private static float Clamp(float v, float min, float max) => Math.Max(min, Math.Min(max, v));

        private float SpanForRange(float from, float to)
        {
            float total = MaxValue - MinValue;
            if (total <= 0f) return 0f;
            return ((Clamp(to, MinValue, MaxValue) - Clamp(from, MinValue, MaxValue)) / total) * ScaleSweepAngle;
        }

        private static double DegToRad(float deg) => (Math.PI / 180.0) * deg;

        private static int NiceNumber(float raw)
        {
            if (raw <= 0f) return 1;
            double exponent = Math.Floor(Math.Log10(raw));
            double pow = Math.Pow(10.0, exponent);
            double normalized = raw / pow;
            double niceFraction;
            if (normalized <= 1.0) niceFraction = 1.0;
            else if (normalized <= 2.0) niceFraction = 2.0;
            else if (normalized <= 5.0) niceFraction = 5.0;
            else niceFraction = 10.0;
            return Math.Max(1, (int)(niceFraction * pow));
        }
        #endregion
    }
}