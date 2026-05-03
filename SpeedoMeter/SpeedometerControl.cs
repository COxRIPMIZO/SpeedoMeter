using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SpeedoMeter
{
    public enum NeedleStyle
    {
        ModernIndicator, ModernThin, ModernBlocky, ModernSleek, ModernCutout, ModernDualLine,
        ClassicTapered, ClassicWide,
        Arrowhead, Broadhead, CrossbowBolt,
        VintageThinWithTail, VintageArtDeco, VintageCrescentTail, VintageAviator,
        OrnateDiamond, OrnateSpearTip, OrnateFleurDeLis, OrnateSword,
        CompassNorth, AnchorTail, Harpoon,
        SciFiArrow, SciFiDataSpike, EnergyBlade,
        SimplePin, KiteTail
    }

    public partial class SpeedometerControl : UserControl
    {
        #region --- Backing Fields for All Properties ---
        private float _value = 0f;
        private float _minValue = 0f;
        private float _maxValue = 450f;
        private string _unitLabel = "M/min";
        private float _scaleStartAngle = 135f;
        private float _scaleSweepAngle = 270f;
        private int _gaugePadding = 10;
        private float _dialPadding = 1f;
        private float _tickZonePadding = 5f;
        private float _tickLabelSpacing = 7f;
        private PointF _valuePosition = new PointF(0.5f, 0.85f);
        private float _bezelWidthMultiplier = 0.08f;
        private Color _dialColor1 = Color.White;
        private Color _dialColor2 = Color.FromArgb(224, 224, 224);
        private Color _bezelColor1 = Color.White;
        private Color _bezelColor2 = Color.White;
        private Color _bezelHighlightColor = Color.White;
        private Color _bezelShadowColor = Color.White;
        private float _zoneGreenEndValue = 100f;
        private float _zoneRedStartValue = 250f;
        private Color _zoneGreenColor = Color.FromArgb(0, 200, 83);
        private Color _zoneYellowColor = Color.FromArgb(255, 193, 7);
        private Color _zoneRedColor = Color.FromArgb(244, 67, 54);
        private float _zoneThicknessMultiplier = 0.02f;
        private float _zoneMinThickness = 2f;
        private int _majorTickStep = 50;
        private int _desiredTickLabelCount = 8;
        private int _minorTicksPerMajor = 5;
        private float _majorTickLengthMultiplier = 0.09f;
        private float _minorTickLengthMultiplier = 0.05f;
        private float _majorTickWidth = 5f;
        private float _minorTickWidth = 2f;
        private string _tickLabelFontFamily = "Segoe UI";
        private float _tickLabelFontSizeMultiplier = 0.05f;
        private float _tickLabelMinFontSize = 10f;
        private Color _tickLabelColor = Color.FromArgb(220, 60, 60, 60);
        private Color _majorTickColor = Color.FromArgb(220, 60, 60, 60);
        private Color _minorTickColor = Color.FromArgb(160, 60, 60, 60);
        private string _valueFontFamily = "Segoe UI";
        private float _valueFontSizeMultiplier = 0.1f;
        private float _unitFontSizeScale = 0.4f;
        private float _valueUnitVerticalSpacing = -12f;
        private float _valueMinFontSize = 18f;
        private float _unitMinFontSize = 10f;
        private Color _valueTextColor = Color.FromArgb(220, 40, 40, 40);
        private Color _unitTextColor = Color.FromArgb(180, 80, 80, 80);
        private NeedleStyle _needleStyle = NeedleStyle.ModernThin;
        private int _needleTransparency = 180;
        private float _needleTipMultiplier = 0.75f;
        private float _needleTailMultiplier = 0.20f;
        private float _needleTailSplitDistanceMultiplier = 0.24f;
        private float _needleShoulderDistanceMultiplier = 0.08f;
        private float _needleShoulderWidthMultiplier = 0.055f;
        private float _needleTailWidthMultiplier = 0.03f;
        private float _needleHighlightWidth = 2.0f;
        private float _needleShadowOffsetMultiplier = 0.02f;
        private Color _needleColor1 = Color.Red;
        private Color _needleColor2 = Color.Red;
        private Color _needleHighlightColor1 = Color.FromArgb(200, 255, 255, 255);
        private Color _needleHighlightColor2 = Color.FromArgb(0, 255, 255, 255);
        private Color _needleShadowColor = Color.FromArgb(72, 0, 0, 0);
        private float _blockyNeedleWidthMultiplier = 0.1f;
        private float _classicNeedleBaseWidthMultiplier = 0.15f;
        private float _ornateDiamondSizeMultiplier = 0.1f;
        private float _ornateCircleTailRadiusMultiplier = 0.08f;
        private float _arrowheadSizeMultiplier = 0.1f;
        private float _dualLineGapMultiplier = 0.02f;
        private float _kiteTailSizeMultiplier = 0.1f;
        private float _indicatorTipWidthMultiplier = 0.05f;
        private float _indicatorTipLengthMultiplier = 0.1f;
        private Color _indicatorHubColor = Color.FromArgb(40, 40, 40);
        private Color _indicatorHubHighlightColor = Color.FromArgb(100, 100, 100);
        private float _hubSizeMultiplier = 0.12f;
        private float _hubMinSize = 12f;
        private Color _hubColor1 = Color.FromArgb(224, 224, 224);
        private Color _hubColor2 = Color.Transparent;
        private Color _hubShadowColor = Color.FromArgb(50, 0, 0, 0);
        private Color _hubHighlightColor = Color.FromArgb(150, 255, 255, 255);
        private Color _outerBorderColor = Color.FromArgb(120, 80, 80, 80);
        private float _outerBorderWidth = 1.5f;
        private Color _outerShadowColor = Color.FromArgb(50, 0, 0, 0);
        private Color _innerShadowColor = Color.FromArgb(100, 0, 0, 0);
        private Color _textShadowColor = Color.FromArgb(30, 0, 0, 0);
        private Color _glassEffectCenterColor = Color.Transparent;
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

        #region Dial Appearance
        [Category("3. Dial Appearance")][Description("The center color of the dial's background gradient.")] public Color DialColor1 { get => _dialColor1; set { _dialColor1 = value; Invalidate(); } }
        [Category("3. Dial Appearance")][Description("The outer edge color of the dial's background gradient.")] public Color DialColor2 { get => _dialColor2; set { _dialColor2 = value; Invalidate(); } }
        [Category("3. Dial Appearance")][Description("The start color for the main bezel gradient.")] public Color BezelColor1 { get => _bezelColor1; set { _bezelColor1 = value; Invalidate(); } }
        [Category("3. Dial Appearance")][Description("The end color for the main bezel gradient.")] public Color BezelColor2 { get => _bezelColor2; set { _bezelColor2 = value; Invalidate(); } }
        [Category("3. Dial Appearance")][Description("The color of the bezel's top-left highlight.")] public Color BezelHighlightColor { get => _bezelHighlightColor; set { _bezelHighlightColor = value; Invalidate(); } }
        [Category("3. Dial Appearance")][Description("The color of the bezel's bottom-right shadow.")] public Color BezelShadowColor { get => _bezelShadowColor; set { _bezelShadowColor = value; Invalidate(); } }
        #endregion

        #region Zones
        [Category("4. Gauge Zones")] public float ZoneGreenEndValue { get => _zoneGreenEndValue; set { _zoneGreenEndValue = value; Invalidate(); } }
        [Category("4. Gauge Zones")] public float ZoneRedStartValue { get => _zoneRedStartValue; set { _zoneRedStartValue = value; Invalidate(); } }
        [Category("4. Gauge Zones")] public Color ZoneGreenColor { get => _zoneGreenColor; set { _zoneGreenColor = value; Invalidate(); } }
        [Category("4. Gauge Zones")] public Color ZoneYellowColor { get => _zoneYellowColor; set { _zoneYellowColor = value; Invalidate(); } }
        [Category("4. Gauge Zones")] public Color ZoneRedColor { get => _zoneRedColor; set { _zoneRedColor = value; Invalidate(); } }
        [Category("4. Gauge Zones")][Description("The thickness of the colored zone arcs, as a multiplier of the gauge radius.")] public float ZoneThicknessMultiplier { get => _zoneThicknessMultiplier; set { _zoneThicknessMultiplier = value; Invalidate(); } }
        [Category("4. Gauge Zones")][Description("The minimum thickness of the zones in pixels.")] public float ZoneMinThickness { get => _zoneMinThickness; set { _zoneMinThickness = value; Invalidate(); } }
        #endregion

        #region Ticks & Labels
        [Category("5. Gauge Ticks")][Description("Overrides the automatic calculation for the major tick step. Set to 0 for auto.")] public int MajorTickStep { get => _majorTickStep; set { _majorTickStep = value; Invalidate(); } }
        [Category("5. Gauge Ticks")][Description("The desired number of labels to show when MajorTickStep is set to auto (0).")] public int DesiredTickLabelCount { get => _desiredTickLabelCount; set { _desiredTickLabelCount = value; Invalidate(); } }
        [Category("5. Gauge Ticks")][Description("The number of minor ticks to show between major ticks.")] public int MinorTicksPerMajor { get => _minorTicksPerMajor; set { _minorTicksPerMajor = value; Invalidate(); } }
        [Category("5. Gauge Ticks")][Description("The length of major ticks, as a multiplier of the gauge radius.")] public float MajorTickLengthMultiplier { get => _majorTickLengthMultiplier; set { _majorTickLengthMultiplier = value; Invalidate(); } }
        [Category("5. Gauge Ticks")][Description("The length of minor ticks, as a multiplier of the gauge radius.")] public float MinorTickLengthMultiplier { get => _minorTickLengthMultiplier; set { _minorTickLengthMultiplier = value; Invalidate(); } }
        [Category("5. Gauge Ticks")][Description("The width of major ticks in pixels.")] public float MajorTickWidth { get => _majorTickWidth; set { _majorTickWidth = value; Invalidate(); } }
        [Category("5. Gauge Ticks")][Description("The width of minor ticks in pixels.")] public float MinorTickWidth { get => _minorTickWidth; set { _minorTickWidth = value; Invalidate(); } }
        [Category("5. Gauge Ticks")] public string TickLabelFontFamily { get => _tickLabelFontFamily; set { _tickLabelFontFamily = value; Invalidate(); } }
        [Category("5. Gauge Ticks")][Description("A multiplier for the font size of the tick mark labels.")] public float TickLabelFontSizeMultiplier { get => _tickLabelFontSizeMultiplier; set { _tickLabelFontSizeMultiplier = value; Invalidate(); } }
        [Category("5. Gauge Ticks")][Description("Minimum font size for tick labels in points.")] public float TickLabelMinFontSize { get => _tickLabelMinFontSize; set { _tickLabelMinFontSize = value; Invalidate(); } }
        [Category("5. Gauge Ticks")] public Color TickLabelColor { get => _tickLabelColor; set { _tickLabelColor = value; Invalidate(); } }
        [Category("5. Gauge Ticks")] public Color MajorTickColor { get => _majorTickColor; set { _majorTickColor = value; Invalidate(); } }
        [Category("5. Gauge Ticks")] public Color MinorTickColor { get => _minorTickColor; set { _minorTickColor = value; Invalidate(); } }
        #endregion

        #region Value Display
        [Category("6. Gauge Value Display")] public string ValueFontFamily { get => _valueFontFamily; set { _valueFontFamily = value; Invalidate(); } }
        [Category("6. Gauge Value Display")][Description("A multiplier for the font size of the main value text.")] public float ValueFontSizeMultiplier { get => _valueFontSizeMultiplier; set { _valueFontSizeMultiplier = value; Invalidate(); } }
        [Category("6. Gauge Value Display")][Description("A multiplier for the font size of the unit text, relative to the value text size.")] public float UnitFontSizeScale { get => _unitFontSizeScale; set { _unitFontSizeScale = value; Invalidate(); } }
        [Category("6. Gauge Value Display")][Description("The vertical spacing in pixels between the value and unit text.")] public float ValueUnitVerticalSpacing { get => _valueUnitVerticalSpacing; set { _valueUnitVerticalSpacing = value; Invalidate(); } }
        [Category("6. Gauge Value Display")][Description("Minimum font size for the main value in points.")] public float ValueMinFontSize { get => _valueMinFontSize; set { _valueMinFontSize = value; Invalidate(); } }
        [Category("6. Gauge Value Display")][Description("Minimum font size for the unit text in points.")] public float UnitMinFontSize { get => _unitMinFontSize; set { _unitMinFontSize = value; Invalidate(); } }
        [Category("6. Gauge Value Display")] public Color ValueTextColor { get => _valueTextColor; set { _valueTextColor = value; Invalidate(); } }
        [Category("6. Gauge Value Display")] public Color UnitTextColor { get => _unitTextColor; set { _unitTextColor = value; Invalidate(); } }
        #endregion

        #region Needle Shape & Color
        [Category("7. Needle")][Description("The visual style of the indicator needle.")] public NeedleStyle NeedleStyle { get => _needleStyle; set { _needleStyle = value; Invalidate(); } }
        [Category("7. Needle")][Description("The transparency of the needle, from 0 (transparent) to 255 (opaque).")] public int NeedleTransparency { get => _needleTransparency; set { _needleTransparency = value; Invalidate(); } }
        [Category("7. Needle")][Description("The distance of the needle tip from the center, as a multiplier of the radius.")] public float NeedleTipMultiplier { get => _needleTipMultiplier; set { _needleTipMultiplier = value; Invalidate(); } }
        [Category("7. Needle")][Description("The distance of the needle tail from the center, as a multiplier of the radius.")] public float NeedleTailMultiplier { get => _needleTailMultiplier; set { _needleTailMultiplier = value; Invalidate(); } }
        [Category("7. Needle")][Description("The distance of the forked tail section from the center, as a multiplier of the radius (ModernThin style).")] public float NeedleTailSplitDistanceMultiplier { get => _needleTailSplitDistanceMultiplier; set { _needleTailSplitDistanceMultiplier = value; Invalidate(); } }
        [Category("7. Needle")][Description("The distance of the needle's 'shoulders' from the center, as a multiplier of the radius (ModernThin style).")] public float NeedleShoulderDistanceMultiplier { get => _needleShoulderDistanceMultiplier; set { _needleShoulderDistanceMultiplier = value; Invalidate(); } }
        [Category("7. Needle")][Description("The width of the needle at its shoulders, as a multiplier of the radius (ModernThin style).")] public float NeedleShoulderWidthMultiplier { get => _needleShoulderWidthMultiplier; set { _needleShoulderWidthMultiplier = value; Invalidate(); } }
        [Category("7. Needle")][Description("The width of the needle at its tail, as a multiplier of the radius (ModernThin style).")] public float NeedleTailWidthMultiplier { get => _needleTailWidthMultiplier; set { _needleTailWidthMultiplier = value; Invalidate(); } }
        [Category("7. Needle")][Description("The width of the central highlight on the needle.")] public float NeedleHighlightWidth { get => _needleHighlightWidth; set { _needleHighlightWidth = value; Invalidate(); } }
        [Category("7. Needle")][Description("The offset of the needle's shadow, as a multiplier of the radius.")] public float NeedleShadowOffsetMultiplier { get => _needleShadowOffsetMultiplier; set { _needleShadowOffsetMultiplier = value; Invalidate(); } }
        [Category("7. Needle")][Description("The main color of the needle.")] public Color NeedleColor1 { get => _needleColor1; set { _needleColor1 = value; Invalidate(); } }
        [Category("7. Needle")][Description("The shaded color of the needle.")] public Color NeedleColor2 { get => _needleColor2; set { _needleColor2 = value; Invalidate(); } }
        [Category("7. Needle")][Description("The start color of the central highlight on the needle.")] public Color NeedleHighlightColor1 { get => _needleHighlightColor1; set { _needleHighlightColor1 = value; Invalidate(); } }
        [Category("7. Needle")][Description("The end color of the central highlight on the needle.")] public Color NeedleHighlightColor2 { get => _needleHighlightColor2; set { _needleHighlightColor2 = value; Invalidate(); } }
        [Category("7. Needle")][Description("The color of the needle's drop shadow.")] public Color NeedleShadowColor { get => _needleShadowColor; set { _needleShadowColor = value; Invalidate(); } }
        #endregion

        #region Needle Shape (Style Specific)
        [Category("7a. Needle Shape (Style Specific)")][Description("The width of the ModernBlocky needle, as a multiplier of the radius.")] public float BlockyNeedleWidthMultiplier { get => _blockyNeedleWidthMultiplier; set { _blockyNeedleWidthMultiplier = value; Invalidate(); } }
        [Category("7a. Needle Shape (Style Specific)")][Description("The width of the base for various tapered needles, as a multiplier of the radius.")] public float ClassicNeedleBaseWidthMultiplier { get => _classicNeedleBaseWidthMultiplier; set { _classicNeedleBaseWidthMultiplier = value; Invalidate(); } }
        [Category("7a. Needle Shape (Style Specific)")][Description("The size of the diamond tip for the OrnateDiamond needle, as a multiplier of the radius.")] public float OrnateDiamondSizeMultiplier { get => _ornateDiamondSizeMultiplier; set { _ornateDiamondSizeMultiplier = value; Invalidate(); } }
        [Category("7a. Needle Shape (Style Specific)")][Description("The radius of the tail circle for the OrnateCircleTail needle, as a multiplier of the radius.")] public float OrnateCircleTailRadiusMultiplier { get => _ornateCircleTailRadiusMultiplier; set { _ornateCircleTailRadiusMultiplier = value; Invalidate(); } }
        [Category("7a. Needle Shape (Style Specific)")][Description("The size of the arrowhead for arrow-like styles, as a multiplier of the radius.")] public float ArrowheadSizeMultiplier { get => _arrowheadSizeMultiplier; set { _arrowheadSizeMultiplier = value; Invalidate(); } }
        [Category("7a. Needle Shape (Style Specific)")][Description("The gap between lines for the ModernDualLine style, as a multiplier of the radius.")] public float DualLineGapMultiplier { get => _dualLineGapMultiplier; set { _dualLineGapMultiplier = value; Invalidate(); } }
        [Category("7a. Needle Shape (Style Specific)")][Description("The size of the tail for various tail styles, as a multiplier of the radius.")] public float KiteTailSizeMultiplier { get => _kiteTailSizeMultiplier; set { _kiteTailSizeMultiplier = value; Invalidate(); } }
        [Category("7a. Needle Shape (Style Specific)")][Description("The width of the blocky tip for the ModernIndicator style.")] public float IndicatorTipWidthMultiplier { get => _indicatorTipWidthMultiplier; set { _indicatorTipWidthMultiplier = value; Invalidate(); } }
        [Category("7a. Needle Shape (Style Specific)")][Description("The length of the blocky tip for the ModernIndicator style.")] public float IndicatorTipLengthMultiplier { get => _indicatorTipLengthMultiplier; set { _indicatorTipLengthMultiplier = value; Invalidate(); } }
        [Category("7a. Needle Shape (Style Specific)")][Description("The main color of the hub for the ModernIndicator style.")] public Color IndicatorHubColor { get => _indicatorHubColor; set { _indicatorHubColor = value; Invalidate(); } }
        [Category("7a. Needle Shape (Style Specific)")][Description("The highlight color of the hub for the ModernIndicator style.")] public Color IndicatorHubHighlightColor { get => _indicatorHubHighlightColor; set { _indicatorHubHighlightColor = value; Invalidate(); } }
        #endregion

        #region Hub
        [Category("8. Hub")][Description("The size of the central hub, as a multiplier of the radius.")] public float HubSizeMultiplier { get => _hubSizeMultiplier; set { _hubSizeMultiplier = value; Invalidate(); } }
        [Category("8. Hub")][Description("Minimum size of the hub in pixels.")] public float HubMinSize { get => _hubMinSize; set { _hubMinSize = value; Invalidate(); } }
        [Category("8. Hub")][Description("The start color of the hub's gradient.")] public Color HubColor1 { get => _hubColor1; set { _hubColor1 = value; Invalidate(); } }
        [Category("8. Hub")][Description("The end color of the hub's gradient.")] public Color HubColor2 { get => _hubColor2; set { _hubColor2 = value; Invalidate(); } }
        [Category("8. Hub")][Description("The color of the hub's outer shadow/border.")] public Color HubShadowColor { get => _hubShadowColor; set { _hubShadowColor = value; Invalidate(); } }
        [Category("8. Hub")][Description("The color of the hub's inner highlight.")] public Color HubHighlightColor { get => _hubHighlightColor; set { _hubHighlightColor = value; Invalidate(); } }
        #endregion

        #region Advanced Colors & Effects
        [Category("9. Advanced Effects")][Description("The color of the thin outer border.")] public Color OuterBorderColor { get => _outerBorderColor; set { _outerBorderColor = value; Invalidate(); } }
        [Category("9. Advanced Effects")][Description("The width of the thin outer border.")] public float OuterBorderWidth { get => _outerBorderWidth; set { _outerBorderWidth = value; Invalidate(); } }
        [Category("9. Advanced Effects")][Description("The center color of the outer drop shadow.")] public Color OuterShadowColor { get => _outerShadowColor; set { _outerShadowColor = value; Invalidate(); } }
        [Category("9. Advanced Effects")][Description("The center color of the shadow inside the bezel.")] public Color InnerShadowColor { get => _innerShadowColor; set { _innerShadowColor = value; Invalidate(); } }
        [Category("9. Advanced Effects")][Description("The color of the drop shadow for ticks and labels.")] public Color TextShadowColor { get => _textShadowColor; set { _textShadowColor = value; Invalidate(); } }
        [Category("9. Advanced Effects")][Description("The center color of the glass highlight effect.")] public Color GlassEffectCenterColor { get => _glassEffectCenterColor; set { _glassEffectCenterColor = value; Invalidate(); } }
        [Category("9. Advanced Effects")][Description("The outer color of the glass highlight effect.")] public Color GlassEffectOuterColor { get => _glassEffectOuterColor; set { _glassEffectOuterColor = value; Invalidate(); } }
        [Category("9. Advanced Effects")][Description("The bounding box of the glass effect, as multipliers of the gauge width/height. (X, Y, Width, Height)")] public RectangleF GlassEffectRect { get => _glassEffectRect; set { _glassEffectRect = value; Invalidate(); } }
        #endregion

        #endregion

        public SpeedometerControl()
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

        private void DrawOuterShadow(Graphics g, Rectangle rect) { using (var p = new GraphicsPath()) { var shadowRect = RectangleF.Inflate(rect, rect.Width * 0.03f, rect.Height * 0.03f); p.AddEllipse(shadowRect); using (var pb = new PathGradientBrush(p)) { pb.CenterColor = OuterShadowColor; pb.SurroundColors = new[] { Color.FromArgb(0, OuterShadowColor) }; pb.FocusScales = new PointF(0.85f, 0.85f); g.FillPath(pb, p); } } }
        private void DrawFace(Graphics g, Rectangle rect) { using (var facePath = new GraphicsPath()) { facePath.AddEllipse(rect); using (var faceBrush = new PathGradientBrush(facePath)) { faceBrush.CenterColor = DialColor1; faceBrush.SurroundColors = new[] { DialColor2 }; faceBrush.FocusScales = new PointF(0.7f, 0.7f); g.FillPath(faceBrush, facePath); } } }

        private void DrawBezel(Graphics g, Rectangle rect)
        {
            float r = rect.Width * 0.5f;
            float w = Math.Max(4f, r * BezelWidthMultiplier);
            var rr = RectangleF.Inflate(rect, -w / 2f, -w / 2f);
            using (var p = new GraphicsPath())
            {
                p.AddEllipse(rr);
                using (var b = new LinearGradientBrush(rect, _bezelColor1, _bezelColor2, 45f))
                    g.FillPath(b, p);
                using (var b = new LinearGradientBrush(rect, _bezelHighlightColor, Color.Transparent, 225f))
                    g.FillPath(b, p);
                using (var b = new LinearGradientBrush(rect, Color.Transparent, _bezelShadowColor, 45f))
                    g.FillPath(b, p);
            }
        }

        private void DrawInnerBezelShadow(Graphics g, Rectangle rect) { float r = rect.Width * 0.5f; float w = Math.Max(4f, r * BezelWidthMultiplier); var sr = RectangleF.Inflate(rect, -w * 0.9f, -w * 0.9f); using (var p = new GraphicsPath()) { p.AddEllipse(rect); p.AddEllipse(sr); using (var sb = new PathGradientBrush(p)) { sb.CenterColor = InnerShadowColor; sb.SurroundColors = new[] { Color.FromArgb(0, InnerShadowColor) }; g.FillPath(sb, p); } } }
        private void DrawGlassEffect(Graphics g, Rectangle rect) { var eff = GlassEffectRect; var hr = new RectangleF(rect.Left + rect.Width * eff.X, rect.Top + rect.Height * eff.Y, rect.Width * eff.Width, rect.Height * eff.Height); using (var p = new GraphicsPath()) { p.AddEllipse(hr); using (var b = new PathGradientBrush(p)) { b.CenterColor = GlassEffectCenterColor; b.SurroundColors = new[] { GlassEffectOuterColor }; b.FocusScales = new PointF(0.5f, 0.5f); g.FillPath(b, p); } } }
        private void DrawZones(Graphics g, Rectangle rect) { float gs = SpanForRange(MinValue, ZoneGreenEndValue), ys = SpanForRange(ZoneGreenEndValue, ZoneRedStartValue), rs = SpanForRange(ZoneRedStartValue, MaxValue); float r = rect.Width * 0.5f, bw = r * BezelWidthMultiplier, zt = Math.Max(ZoneMinThickness, r * ZoneThicknessMultiplier), ti = bw + DialPadding + (zt / 2f); var zr = RectangleF.Inflate(rect, -ti, -ti); using (var pG = new Pen(ZoneGreenColor, zt)) using (var pY = new Pen(ZoneYellowColor, zt)) using (var pR = new Pen(ZoneRedColor, zt)) { pG.EndCap = pY.EndCap = pR.EndCap = LineCap.Round; if (gs > 0) g.DrawArc(pG, zr, ScaleStartAngle, gs); if (ys > 0) g.DrawArc(pY, zr, ScaleStartAngle + gs, ys); if (rs > 0) g.DrawArc(pR, zr, ScaleStartAngle + gs + ys, rs); } }
        private void DrawTicks(Graphics g, Rectangle rect) { float r = rect.Width * 0.5f; var c = new PointF(rect.Left + r, rect.Top + r); float vr = Math.Max(1f, MaxValue - MinValue); int M = (MajorTickStep > 0) ? MajorTickStep : NiceNumber(vr / Math.Max(1, DesiredTickLabelCount)), m = Math.Max(1, M / Math.Max(1, MinorTicksPerMajor)); float bw = r * BezelWidthMultiplier, zt = Math.Max(ZoneMinThickness, r * ZoneThicknessMultiplier), ot = bw + DialPadding + zt + TickZonePadding, Mtl = r * MajorTickLengthMultiplier, mtl = r * MinorTickLengthMultiplier, Mit = ot + Mtl, mit = ot + mtl, fs = Math.Max(TickLabelMinFontSize, r * TickLabelFontSizeMultiplier); using (var tf = new Font(TickLabelFontFamily, fs, FontStyle.Bold)) using (var tb = new SolidBrush(TickLabelColor)) using (var sb = new SolidBrush(TextShadowColor)) using (var Mp = new Pen(MajorTickColor, MajorTickWidth)) using (var mp = new Pen(MinorTickColor, MinorTickWidth)) { for (int v = (int)(Math.Ceiling(MinValue / m) * m); v <= (int)(Math.Floor(MaxValue / m) * m); v += m) { bool iM = (v % M == 0); g.DrawLine(iM ? Mp : mp, PointOnCircle(c, r - ot, v), PointOnCircle(c, r - (iM ? Mit : mit), v)); } for (int v = (int)(Math.Ceiling(MinValue / M) * M); v <= MaxValue; v += M) DrawLabelAtValue(g, c, r, v, Mit, tf, tb, sb); DrawLabelAtValue(g, c, r, (int)MinValue, Mit, tf, tb, sb); DrawLabelAtValue(g, c, r, (int)MaxValue, Mit, tf, tb, sb); } }
        private void DrawLabelAtValue(Graphics g, PointF c, float r, int v, float t, Font f, Brush b, Brush sb) { string l = v.ToString(); SizeF s = g.MeasureString(l, f); float lci = t + TickLabelSpacing + (s.Height / 2f); var p = PointOnCircle(c, r - lci, v); g.DrawString(l, f, sb, p.X - s.Width / 2f + 1, p.Y - s.Height / 2f + 1); g.DrawString(l, f, b, p.X - s.Width / 2f, p.Y - s.Height / 2f); }
        private void DrawValueLabel(Graphics g, Rectangle rect) { string vt = ((int)_value).ToString(), ut = _unitLabel; float r = rect.Width * 0.5f; var bc = new PointF(rect.Left + rect.Width * ValuePosition.X, rect.Top + rect.Height * ValuePosition.Y); float fsv = Math.Max(ValueMinFontSize, r * ValueFontSizeMultiplier), fsu = Math.Max(UnitMinFontSize, fsv * UnitFontSizeScale); using (var vf = new Font(ValueFontFamily, fsv, FontStyle.Bold)) using (var uf = new Font(ValueFontFamily, fsu, FontStyle.Regular)) using (var vb = new SolidBrush(ValueTextColor)) using (var ub = new SolidBrush(UnitTextColor)) { var vs = g.MeasureString(vt, vf); var us = g.MeasureString(ut, uf); float tbh = vs.Height + us.Height + ValueUnitVerticalSpacing, vty = bc.Y - tbh / 2f, uty = vty + vs.Height + ValueUnitVerticalSpacing; g.DrawString(vt, vf, vb, bc.X - vs.Width / 2f, vty); g.DrawString(ut, uf, ub, bc.X - us.Width / 2f, uty); } }
        private void DrawThinBorder(Graphics g, Rectangle rect) { using (var pen = new Pen(OuterBorderColor, OuterBorderWidth)) g.DrawEllipse(pen, rect); }
        #endregion

        #region --- Needle Drawing Logic ---
        private void DrawNeedle(Graphics g, Rectangle rect)
        {
            switch (NeedleStyle)
            {
                case NeedleStyle.ModernIndicator: DrawNeedle_ModernIndicator(g, rect); break;
                //case NeedleStyle.ModernBlocky: DrawNeedle_ModernBlocky(g, rect); break;
                case NeedleStyle.ClassicTapered: DrawNeedle_ClassicTapered(g, rect); break;
                case NeedleStyle.ClassicWide: DrawNeedle_ClassicWide(g, rect); break;
                case NeedleStyle.OrnateDiamond: DrawNeedle_OrnateDiamond(g, rect); break;
                //case NeedleStyle.OrnateCircleTail: DrawNeedle_OrnateCircleTail(g, rect); break;
                case NeedleStyle.ModernSleek: DrawNeedle_ModernSleek(g, rect); break;
                case NeedleStyle.ModernCutout: DrawNeedle_ModernCutout(g, rect); break;
                case NeedleStyle.ModernDualLine: DrawNeedle_ModernDualLine(g, rect); break;
                case NeedleStyle.Arrowhead: DrawNeedle_Arrowhead(g, rect); break;
                case NeedleStyle.Broadhead: DrawNeedle_Broadhead(g, rect); break;
                case NeedleStyle.CrossbowBolt: DrawNeedle_CrossbowBolt(g, rect); break;
                case NeedleStyle.VintageThinWithTail: DrawNeedle_VintageThinWithTail(g, rect); break;
                case NeedleStyle.VintageArtDeco: DrawNeedle_VintageArtDeco(g, rect); break;
                case NeedleStyle.VintageCrescentTail: DrawNeedle_VintageCrescentTail(g, rect); break;
                case NeedleStyle.VintageAviator: DrawNeedle_VintageAviator(g, rect); break;
                case NeedleStyle.OrnateSpearTip: DrawNeedle_OrnateSpearTip(g, rect); break;
                case NeedleStyle.OrnateFleurDeLis: DrawNeedle_OrnateFleurDeLis(g, rect); break;
                case NeedleStyle.OrnateSword: DrawNeedle_OrnateSword(g, rect); break;
                case NeedleStyle.CompassNorth: DrawNeedle_CompassNorth(g, rect); break;
                case NeedleStyle.AnchorTail: DrawNeedle_AnchorTail(g, rect); break;
                case NeedleStyle.Harpoon: DrawNeedle_Harpoon(g, rect); break;
                case NeedleStyle.SciFiArrow: DrawNeedle_SciFiArrow(g, rect); break;
                case NeedleStyle.SciFiDataSpike: DrawNeedle_SciFiDataSpike(g, rect); break;
                case NeedleStyle.EnergyBlade: DrawNeedle_EnergyBlade(g, rect); break;
                case NeedleStyle.SimplePin: DrawNeedle_SimplePin(g, rect); break;
                //case NeedleStyle.KiteTail: DrawNeedle_KiteTail(g, rect); break;
                case NeedleStyle.ModernThin: default: DrawNeedle_ModernThin(g, rect); break;
            }
        }
        private void DrawNeedleShadowAndFill(Graphics g, GraphicsPath path, float angle, float radius) { using (var sp = (GraphicsPath)path.Clone()) { var m = new Matrix(); m.Translate(radius * NeedleShadowOffsetMultiplier, radius * NeedleShadowOffsetMultiplier); sp.Transform(m); using (var sb = new SolidBrush(NeedleShadowColor)) g.FillPath(sb, sp); } using (var b = new LinearGradientBrush(path.GetBounds(), Color.FromArgb(NeedleTransparency, NeedleColor1), Color.FromArgb(NeedleTransparency, NeedleColor2), angle + 90)) g.FillPath(b, path); }
        private void DrawNeedleShadowAndFill(Graphics g, Pen pen, GraphicsPath path, float angle, float radius) { using (var sp = (GraphicsPath)path.Clone()) { var m = new Matrix(); m.Translate(radius * NeedleShadowOffsetMultiplier, radius * NeedleShadowOffsetMultiplier); sp.Transform(m); using (var spen = new Pen(NeedleShadowColor, pen.Width)) g.DrawPath(spen, sp); } g.DrawPath(pen, path); }
        private void DrawNeedleHub(Graphics g, PointF center, float radius) { float hr = Math.Max(HubMinSize, radius * HubSizeMultiplier); var hrect = new RectangleF(center.X - hr, center.Y - hr, hr * 2, hr * 2); using (var hp = new GraphicsPath()) { hp.AddEllipse(hrect); using (var hb = new LinearGradientBrush(hrect, HubColor1, HubColor2, 45f)) g.FillPath(hb, hp); using (var p = new Pen(HubHighlightColor, 1f)) g.DrawEllipse(p, RectangleF.Inflate(hrect, -2, -2)); using (var p = new Pen(HubShadowColor, 2f)) g.DrawEllipse(p, hrect); } }
        private void DrawNeedleHub_IndicatorStyle(Graphics g, PointF center, float radius) { float hr = Math.Max(HubMinSize, radius * HubSizeMultiplier); var hrect = new RectangleF(center.X - hr, center.Y - hr, hr * 2, hr * 2); using (var hp = new GraphicsPath()) { hp.AddEllipse(hrect); using (var hb = new SolidBrush(IndicatorHubColor)) g.FillPath(hb, hp); using (var ip = new GraphicsPath()) { ip.AddEllipse(RectangleF.Inflate(hrect, -hr * 0.1f, -hr * 0.1f)); using (var pgb = new PathGradientBrush(ip)) { pgb.CenterColor = IndicatorHubHighlightColor; pgb.SurroundColors = new[] { Color.Transparent }; g.FillPath(pgb, ip); } } } }
        private void DrawNeedle_ModernIndicator(Graphics g, Rectangle rect) { float r = rect.Width * 0.5f; var c = new PointF(rect.Left + r, rect.Top + r); float a = MapValueToAngle(Value); var t = PointOnCircle(c, r * NeedleTipMultiplier, Value); using (var p = new Pen(Color.FromArgb(NeedleTransparency, NeedleColor1), 3f)) { g.DrawLine(p, c, t); } float tl = r * IndicatorTipLengthMultiplier, tw = r * IndicatorTipWidthMultiplier; var perp = GetPerpendicularVector(a); var tb = PointOnCircle(c, r * NeedleTipMultiplier - tl, Value); var p1 = new PointF(t.X - perp.X * tw / 2, t.Y - perp.Y * tw / 2); var p2 = new PointF(t.X + perp.X * tw / 2, t.Y + perp.Y * tw / 2); var p3 = new PointF(tb.X + perp.X * tw / 2, tb.Y + perp.Y * tw / 2); var p4 = new PointF(tb.X - perp.X * tw / 2, tb.Y - perp.Y * tw / 2); using (var path = new GraphicsPath()) { path.AddPolygon(new[] { p1, p2, p3, p4 }); using (var b = new SolidBrush(Color.FromArgb(NeedleTransparency, NeedleColor1))) g.FillPath(b, path); } DrawNeedleHub_IndicatorStyle(g, c, r); }
        private void DrawNeedle_ModernThin(Graphics g, Rectangle rect) { float r = rect.Width * 0.5f; var c = new PointF(rect.Left + r, rect.Top + r); float a = MapValueToAngle(Value); var d = GetDirectionVector(a); var p = new PointF(-d.Y, d.X); var t = new PointF(c.X + d.X * (r * NeedleTipMultiplier), c.Y + d.Y * (r * NeedleTipMultiplier)); var sl = new PointF(c.X + d.X * (r * NeedleShoulderDistanceMultiplier) - p.X * (r * NeedleShoulderWidthMultiplier), c.Y + d.Y * (r * NeedleShoulderDistanceMultiplier) - p.Y * (r * NeedleShoulderWidthMultiplier)); var sr = new PointF(c.X + d.X * (r * NeedleShoulderDistanceMultiplier) + p.X * (r * NeedleShoulderWidthMultiplier), c.Y + d.Y * (r * NeedleShoulderDistanceMultiplier) + p.Y * (r * NeedleShoulderWidthMultiplier)); var tl = new PointF(c.X - d.X * (r * NeedleTailSplitDistanceMultiplier) - p.X * (r * NeedleTailWidthMultiplier), c.Y - d.Y * (r * NeedleTailSplitDistanceMultiplier) - p.Y * (r * NeedleTailWidthMultiplier)); var tr = new PointF(c.X - d.X * (r * NeedleTailSplitDistanceMultiplier) + p.X * (r * NeedleTailWidthMultiplier), c.Y - d.Y * (r * NeedleTailSplitDistanceMultiplier) + p.Y * (r * NeedleTailWidthMultiplier)); var tc = new PointF(c.X - d.X * (r * NeedleTailMultiplier), c.Y - d.Y * (r * NeedleTailMultiplier)); using (var path = new GraphicsPath()) { path.AddPolygon(new[] { t, sr, tr, tc, tl, sl }); DrawNeedleShadowAndFill(g, path, a, r); using (var hp = new GraphicsPath()) { hp.AddLine(sl, t); hp.AddLine(t, sr); using (var lgb = new LinearGradientBrush(path.GetBounds(), NeedleHighlightColor1, NeedleHighlightColor2, a)) using (var pen = new Pen(lgb, NeedleHighlightWidth)) g.DrawPath(pen, hp); } } DrawNeedleHub(g, c, r); }
        private void DrawNeedle_SimplePin(Graphics g, Rectangle rect) { float r = rect.Width * 0.5f; var c = new PointF(rect.Left + r, rect.Top + r); float a = MapValueToAngle(Value); var t = PointOnCircle(c, r * NeedleTipMultiplier, Value); using (var p = new GraphicsPath()) { p.AddLine(c, t); DrawNeedleShadowAndFill(g, new Pen(Color.FromArgb(NeedleTransparency, NeedleColor1), 2f), p, a, r); } DrawNeedleHub(g, c, r); }
        private void DrawNeedle_ClassicTapered(Graphics g, Rectangle rect) { float r = rect.Width * 0.5f; var c = new PointF(rect.Left + r, rect.Top + r); float a = MapValueToAngle(Value); var p = GetPerpendicularVector(a); float bw = r * ClassicNeedleBaseWidthMultiplier; var t = PointOnCircle(c, r * NeedleTipMultiplier, Value); var bl = new PointF(c.X - p.X * bw / 2, c.Y - p.Y * bw / 2); var br = new PointF(c.X + p.X * bw / 2, c.Y + p.Y * bw / 2); using (var path = new GraphicsPath()) { path.AddPolygon(new[] { t, br, bl }); DrawNeedleShadowAndFill(g, path, a, r); } DrawNeedleHub(g, c, r); }
        private void DrawNeedle_ClassicWide(Graphics g, Rectangle rect) { float r = rect.Width * 0.5f; var c = new PointF(rect.Left + r, rect.Top + r); float a = MapValueToAngle(Value); var d = GetDirectionVector(a); var p = new PointF(-d.Y, d.X); float bw = r * ClassicNeedleBaseWidthMultiplier, tw = bw * 0.2f; var tl = new PointF(c.X + d.X * (r * NeedleTipMultiplier) - p.X * tw / 2, c.Y + d.Y * (r * NeedleTipMultiplier) - p.Y * tw / 2); var tr = new PointF(c.X + d.X * (r * NeedleTipMultiplier) + p.X * tw / 2, c.Y + d.Y * (r * NeedleTipMultiplier) + p.Y * tw / 2); var bl = new PointF(c.X - p.X * bw / 2, c.Y - p.Y * bw / 2); var br = new PointF(c.X + p.X * bw / 2, c.Y + p.Y * bw / 2); using (var path = new GraphicsPath()) { path.AddPolygon(new[] { tr, br, bl, tl }); DrawNeedleShadowAndFill(g, path, a, r); } DrawNeedleHub(g, c, r); }
        private void DrawNeedle_OrnateDiamond(Graphics g, Rectangle rect) { float r = rect.Width * 0.5f; var c = new PointF(rect.Left + r, rect.Top + r); float a = MapValueToAngle(Value); var p = GetPerpendicularVector(a); float ds = r * OrnateDiamondSizeMultiplier; var t = PointOnCircle(c, r * NeedleTipMultiplier, Value); var mp = PointOnCircle(c, r * NeedleTipMultiplier - ds, Value); var sl = new PointF(mp.X - p.X * ds / 2, mp.Y - p.Y * ds / 2); var sr = new PointF(mp.X + p.X * ds / 2, mp.Y + p.Y * ds / 2); var bp = PointOnCircle(c, r * NeedleTipMultiplier - ds * 2, Value); using (var path = new GraphicsPath()) { path.AddLine(c, bp); path.AddPolygon(new[] { t, sr, bp, sl }); DrawNeedleShadowAndFill(g, new Pen(Color.FromArgb(NeedleTransparency, NeedleColor1), 2f), path, a, r); } DrawNeedleHub(g, c, r); }
        private void DrawNeedle_OrnateCircleTail(Graphics g, Rectangle rect) { float r = rect.Width * 0.5f; var c = new PointF(rect.Left + r, rect.Top + r); float a = MapValueToAngle(Value); var p = GetPerpendicularVector(a); float bw = r * ClassicNeedleBaseWidthMultiplier * 0.5f; var t = PointOnCircle(c, r * NeedleTipMultiplier, Value); var bl = new PointF(c.X - p.X * bw / 2, c.Y - p.Y * bw / 2); var br = new PointF(c.X + p.X * bw / 2, c.Y + p.Y * bw / 2); using (var path = new GraphicsPath()) { path.AddPolygon(new[] { t, br, bl }); DrawNeedleShadowAndFill(g, path, a, r); } float tr = r * OrnateCircleTailRadiusMultiplier; var tp = PointOnCircle(c, r * NeedleTailMultiplier, Value, 180); var trect = new RectangleF(tp.X - tr, tp.Y - tr, tr * 2, tr * 2); using (var b = new SolidBrush(Color.FromArgb(NeedleTransparency, NeedleColor1))) g.FillEllipse(b, trect); DrawNeedleHub(g, c, r); }
        private void DrawNeedle_ModernTriangle(Graphics g, Rectangle rect) { DrawNeedle_ClassicTapered(g, rect); }
        private void DrawNeedle_ModernDualLine(Graphics g, Rectangle rect) { float r = rect.Width * 0.5f; var c = new PointF(rect.Left + r, rect.Top + r); float a = MapValueToAngle(Value); var p = GetPerpendicularVector(a); float gap = r * DualLineGapMultiplier; var t = PointOnCircle(c, r * NeedleTipMultiplier, Value); var p1s = new PointF(c.X - p.X * gap, c.Y - p.Y * gap); var p1e = new PointF(t.X - p.X * gap, t.Y - p.Y * gap); var p2s = new PointF(c.X + p.X * gap, c.Y + p.Y * gap); var p2e = new PointF(t.X + p.X * gap, t.Y + p.Y * gap); using (var pen = new Pen(Color.FromArgb(NeedleTransparency, NeedleColor1), 2f)) { g.DrawLine(pen, p1s, p1e); g.DrawLine(pen, p2s, p2e); } DrawNeedleHub(g, c, r); }
        private void DrawNeedle_ModernCutout(Graphics g, Rectangle rect) { float r = rect.Width * 0.5f; var c = new PointF(rect.Left + r, rect.Top + r); float a = MapValueToAngle(Value); var p = GetPerpendicularVector(a); float bw = r * BlockyNeedleWidthMultiplier; var t = PointOnCircle(c, r * NeedleTipMultiplier, Value); var p1 = new PointF(t.X - p.X * bw / 4, t.Y - p.Y * bw / 4); var p2 = new PointF(t.X + p.X * bw / 4, t.Y + p.Y * bw / 4); var p3 = new PointF(c.X + p.X * bw / 2, c.Y + p.Y * bw / 2); var p4 = new PointF(c.X - p.X * bw / 2, c.Y - p.Y * bw / 2); var cp1 = PointOnCircle(c, r * 0.5f, Value); var cp2 = PointOnCircle(c, r * 0.3f, Value); var cp3 = new PointF(cp2.X + p.X * bw / 4, cp2.Y + p.Y * bw / 4); var cp4 = new PointF(cp2.X - p.X * bw / 4, cp2.Y - p.Y * bw / 4); using (var path = new GraphicsPath()) { path.AddPolygon(new[] { p1, p2, p3, p4 }); path.AddPolygon(new[] { cp1, cp3, cp4 }); DrawNeedleShadowAndFill(g, path, a, r); } DrawNeedleHub(g, c, r); }
        private void DrawNeedle_VintageThinWithTail(Graphics g, Rectangle rect) { float r = rect.Width * 0.5f; var c = new PointF(rect.Left + r, rect.Top + r); float a = MapValueToAngle(Value); var tip = PointOnCircle(c, r * NeedleTipMultiplier, Value); using (var p = new GraphicsPath()) { p.AddLine(c, tip); DrawNeedleShadowAndFill(g, new Pen(Color.FromArgb(NeedleTransparency, NeedleColor1), 2f), p, a, r); } var perp = GetPerpendicularVector(a); float ts = r * KiteTailSizeMultiplier; var te = PointOnCircle(c, r * NeedleTailMultiplier, Value, 180); var p1 = new PointF(te.X - perp.X * ts / 2, te.Y - perp.Y * ts / 2); var p2 = new PointF(te.X + perp.X * ts / 2, te.Y + perp.Y * ts / 2); using (var p = new GraphicsPath()) { p.AddPolygon(new[] { c, p2, p1 }); DrawNeedleShadowAndFill(g, p, a, r); } DrawNeedleHub(g, c, r); }
        private void DrawNeedle_VintageArtDeco(Graphics g, Rectangle rect) { float r = rect.Width * 0.5f; var c = new PointF(rect.Left + r, rect.Top + r); float a = MapValueToAngle(Value); var p = GetPerpendicularVector(a); float bw = r * ClassicNeedleBaseWidthMultiplier; using (var path = new GraphicsPath()) { var p1 = PointOnCircle(c, r * NeedleTipMultiplier, Value); var p2 = PointOnCircle(c, r * 0.6f, Value); var p3 = new PointF(p2.X + p.X * bw * 0.2f, p2.Y + p.Y * bw * 0.2f); var p4 = new PointF(p2.X - p.X * bw * 0.2f, p2.Y - p.Y * bw * 0.2f); var p5 = PointOnCircle(c, r * 0.5f, Value); var p6 = new PointF(p5.X + p.X * bw * 0.4f, p5.Y + p.Y * bw * 0.4f); var p7 = new PointF(p5.X - p.X * bw * 0.4f, p5.Y - p.Y * bw * 0.4f); var p8 = new PointF(c.X + p.X * bw / 2, c.Y + p.Y * bw / 2); var p9 = new PointF(c.X - p.X * bw / 2, c.Y - p.Y * bw / 2); path.AddPolygon(new[] { p1, p3, p6, p8, p9, p7, p4 }); DrawNeedleShadowAndFill(g, path, a, r); } DrawNeedleHub(g, c, r); }
        private void DrawNeedle_VintageCrescentTail(Graphics g, Rectangle rect) { float r = rect.Width * 0.5f; var c = new PointF(rect.Left + r, rect.Top + r); DrawNeedle_ClassicTapered(g, rect); float tr = r * OrnateCircleTailRadiusMultiplier * 1.5f; var tp = PointOnCircle(c, r * NeedleTailMultiplier, Value, 180); using (var path = new GraphicsPath()) { path.AddEllipse(tp.X - tr, tp.Y - tr, tr * 2, tr * 2); using (var b = new SolidBrush(BackColor)) g.FillPath(b, path); } }
        private void DrawNeedle_OrnateSpearTip(Graphics g, Rectangle rect) { float r = rect.Width * 0.5f; var c = new PointF(rect.Left + r, rect.Top + r); float a = MapValueToAngle(Value); float ss = r * ArrowheadSizeMultiplier; var t = PointOnCircle(c, r * NeedleTipMultiplier, Value); var boh = PointOnCircle(c, r * NeedleTipMultiplier - ss, Value); var b1 = PointOnCircle(boh, ss * 0.8f, Value, 150); var b2 = PointOnCircle(boh, ss * 0.8f, Value, -150); using (var path = new GraphicsPath()) { path.AddLine(c, boh); path.AddPolygon(new[] { t, b2, boh, b1 }); DrawNeedleShadowAndFill(g, new Pen(Color.FromArgb(NeedleTransparency, NeedleColor1), 2f), path, a, r); } DrawNeedleHub(g, c, r); }
        private void DrawNeedle_OrnateFleurDeLis(Graphics g, Rectangle rect) { float r = rect.Width * 0.5f; var c = new PointF(rect.Left + r, rect.Top + r); float a = MapValueToAngle(Value); var t = PointOnCircle(c, r * NeedleTipMultiplier, Value); using (var p = new GraphicsPath()) { p.AddLine(c, t); DrawNeedleShadowAndFill(g, new Pen(Color.FromArgb(NeedleTransparency, NeedleColor1), 2f), p, a, r); } float ts = r * KiteTailSizeMultiplier * 1.5f; var tb = PointOnCircle(c, r * NeedleTailMultiplier, Value, 180); var p1 = PointOnCircle(tb, ts * 0.5f, Value, 180); var p2 = PointOnCircle(tb, ts, Value, 180 + 90); var p3 = PointOnCircle(tb, ts, Value, 180 - 90); using (var p = new GraphicsPath()) { p.AddCurve(new[] { p2, p1, p3 }); g.DrawPath(new Pen(Color.FromArgb(NeedleTransparency, NeedleColor1), 3f), p); } DrawNeedleHub(g, c, r); }
        private void DrawNeedle_OrnateSword(Graphics g, Rectangle rect) { float r = rect.Width * 0.5f; var c = new PointF(rect.Left + r, rect.Top + r); float a = MapValueToAngle(Value); var p = GetPerpendicularVector(a); float bw = r * ClassicNeedleBaseWidthMultiplier; var t = PointOnCircle(c, r * NeedleTipMultiplier, Value); var m1 = PointOnCircle(c, r * 0.6f, Value); var m2 = PointOnCircle(c, r * 0.5f, Value); var h1 = new PointF(m2.X + p.X * bw / 2, m2.Y + p.Y * bw / 2); var h2 = new PointF(m2.X - p.X * bw / 2, m2.Y - p.Y * bw / 2); using (var path = new GraphicsPath()) { path.AddLine(t, m1); path.AddLine(m1, m2); path.AddLine(h1, h2); path.AddLine(m2, c); DrawNeedleShadowAndFill(g, new Pen(Color.FromArgb(NeedleTransparency, NeedleColor1), 3f), path, a, r); } DrawNeedleHub(g, c, r); }
        private void DrawNeedle_SciFiArrow(Graphics g, Rectangle rect) { float r = rect.Width * 0.5f; var c = new PointF(rect.Left + r, rect.Top + r); float a = MapValueToAngle(Value); float hs = r * ArrowheadSizeMultiplier; var t = PointOnCircle(c, r * NeedleTipMultiplier, Value); var p2 = PointOnCircle(c, r * NeedleTipMultiplier - hs, Value, -2); var p3 = PointOnCircle(c, r * NeedleTipMultiplier - hs, Value, 2); var p4 = PointOnCircle(c, r * NeedleTailMultiplier, Value, 180); using (var path = new GraphicsPath()) { path.AddPolygon(new[] { t, p3, p4, p2 }); DrawNeedleShadowAndFill(g, path, a, r); } DrawNeedleHub(g, c, r); }
        private void DrawNeedle_SciFiDataSpike(Graphics g, Rectangle rect) { float r = rect.Width * 0.5f; var c = new PointF(rect.Left + r, rect.Top + r); var t = PointOnCircle(c, r * NeedleTipMultiplier, Value); var p1 = PointOnCircle(c, r * 0.6f, Value, 2); var p2 = PointOnCircle(c, r * 0.6f, Value, -2); var p3 = PointOnCircle(c, r * 0.3f, Value, 4); var p4 = PointOnCircle(c, r * 0.3f, Value, -4); using (var path = new GraphicsPath()) { path.AddPolygon(new[] { t, p1, p3, c, p4, p2 }); DrawNeedleShadowAndFill(g, path, MapValueToAngle(Value), r); } DrawNeedleHub(g, c, r); }
        private void DrawNeedle_EnergyBlade(Graphics g, Rectangle rect) { float r = rect.Width * 0.5f; var c = new PointF(rect.Left + r, rect.Top + r); float a = MapValueToAngle(Value); var p = GetPerpendicularVector(a); float bw = r * BlockyNeedleWidthMultiplier * 0.5f; var t = PointOnCircle(c, r * NeedleTipMultiplier, Value); var bl = new PointF(c.X - p.X * bw / 2, c.Y - p.Y * bw / 2); var br = new PointF(c.X + p.X * bw / 2, c.Y + p.Y * bw / 2); using (var path = new GraphicsPath()) { path.AddPolygon(new[] { t, br, bl }); using (var pgb = new PathGradientBrush(path)) { pgb.CenterColor = Color.White; pgb.SurroundColors = new[] { Color.FromArgb(NeedleTransparency, NeedleColor1) }; g.FillPath(pgb, path); } } DrawNeedleHub(g, c, r); }
        private void DrawNeedle_ModernSleek(Graphics g, Rectangle rect) { float r = rect.Width * 0.5f; var c = new PointF(rect.Left + r, rect.Top + r); float a = MapValueToAngle(Value); var p = GetPerpendicularVector(a); var t = PointOnCircle(c, r * NeedleTipMultiplier, Value); var s1 = new PointF(c.X + p.X * r * 0.05f, c.Y + p.Y * r * 0.05f); var s2 = new PointF(c.X - p.X * r * 0.05f, c.Y - p.Y * r * 0.05f); var tail = PointOnCircle(c, r * NeedleTailMultiplier, Value, 180); using (var path = new GraphicsPath()) { path.AddCurve(new[] { tail, s2, t, s1, tail }); DrawNeedleShadowAndFill(g, path, a, r); } DrawNeedleHub(g, c, r); }
        private void DrawNeedle_Arrowhead(Graphics g, Rectangle rect) { float r = rect.Width * 0.5f; var c = new PointF(rect.Left + r, rect.Top + r); float a = MapValueToAngle(Value); float hs = r * ArrowheadSizeMultiplier; var t = PointOnCircle(c, r * NeedleTipMultiplier, Value); var boh = PointOnCircle(c, r * NeedleTipMultiplier - hs, Value); var p = GetPerpendicularVector(a); var p1 = new PointF(boh.X - p.X * hs / 2, boh.Y - p.Y * hs / 2); var p2 = new PointF(boh.X + p.X * hs / 2, boh.Y + p.Y * hs / 2); using (var path = new GraphicsPath()) { path.AddLine(c, boh); path.AddPolygon(new[] { t, p2, p1 }); DrawNeedleShadowAndFill(g, path, a, r); } DrawNeedleHub(g, c, r); }
        private void DrawNeedle_Broadhead(Graphics g, Rectangle rect) { float r = rect.Width * 0.5f; var c = new PointF(rect.Left + r, rect.Top + r); float a = MapValueToAngle(Value); float hs = r * ArrowheadSizeMultiplier * 1.5f; var t = PointOnCircle(c, r * NeedleTipMultiplier, Value); var n = PointOnCircle(c, r * NeedleTipMultiplier - hs, Value); var p = GetPerpendicularVector(a); var b1 = new PointF(n.X + p.X * hs * 0.4f, n.Y + p.Y * hs * 0.4f); var b2 = new PointF(n.X - p.X * hs * 0.4f, n.Y - p.Y * hs * 0.4f); var back = PointOnCircle(c, r * NeedleTipMultiplier - hs * 1.2f, Value); using (var path = new GraphicsPath()) { path.AddLine(c, back); path.AddPolygon(new[] { t, b1, back, b2 }); DrawNeedleShadowAndFill(g, path, a, r); } DrawNeedleHub(g, c, r); }
        private void DrawNeedle_CrossbowBolt(Graphics g, Rectangle rect) { float r = rect.Width * 0.5f; var c = new PointF(rect.Left + r, rect.Top + r); float a = MapValueToAngle(Value); float hs = r * ArrowheadSizeMultiplier; var t = PointOnCircle(c, r * NeedleTipMultiplier, Value); var boh = PointOnCircle(c, r * NeedleTipMultiplier - hs, Value); var p = GetPerpendicularVector(a); var p1 = new PointF(boh.X - p.X * hs / 3, boh.Y - p.Y * hs / 3); var p2 = new PointF(boh.X + p.X * hs / 3, boh.Y + p.Y * hs / 3); var tail = PointOnCircle(c, r * NeedleTailMultiplier, Value, 180); var f1 = new PointF(tail.X - p.X * hs / 2, tail.Y - p.Y * hs / 2); var f2 = new PointF(tail.X + p.X * hs / 2, tail.Y + p.Y * hs / 2); using (var path = new GraphicsPath()) { path.AddPolygon(new[] { t, p2, f2, f1, p1 }); DrawNeedleShadowAndFill(g, path, a, r); } DrawNeedleHub(g, c, r); }
        private void DrawNeedle_VintageAviator(Graphics g, Rectangle rect) { float r = rect.Width * 0.5f; var c = new PointF(rect.Left + r, rect.Top + r); float a = MapValueToAngle(Value); var t = PointOnCircle(c, r * NeedleTipMultiplier, Value); var tail = PointOnCircle(c, r * NeedleTailMultiplier, Value, 180); var p = GetPerpendicularVector(a); float w = r * 0.05f; var p1 = new PointF(t.X - p.X * w, t.Y - p.Y * w); var p2 = new PointF(t.X + p.X * w, t.Y + p.Y * w); var p3 = new PointF(tail.X + p.X * w * 2, tail.Y + p.Y * w * 2); var p4 = new PointF(tail.X - p.X * w * 2, tail.Y - p.Y * w * 2); using (var path = new GraphicsPath()) { path.AddPolygon(new[] { p1, p2, p3, p4 }); DrawNeedleShadowAndFill(g, path, a, r); } DrawNeedleHub(g, c, r); }
        private void DrawNeedle_Harpoon(Graphics g, Rectangle rect) { float r = rect.Width * 0.5f; var c = new PointF(rect.Left + r, rect.Top + r); float a = MapValueToAngle(Value); float hs = r * ArrowheadSizeMultiplier; var t = PointOnCircle(c, r * NeedleTipMultiplier, Value); var n = PointOnCircle(c, r * NeedleTipMultiplier - hs, Value); var b1 = PointOnCircle(n, hs * 0.5f, Value, 150); var b2 = PointOnCircle(n, hs * 0.5f, Value, -150); using (var path = new GraphicsPath()) { path.AddLine(c, t); path.AddLine(b1, n); path.AddLine(n, b2); DrawNeedleShadowAndFill(g, new Pen(Color.FromArgb(NeedleTransparency, NeedleColor1), 3f), path, a, r); } DrawNeedleHub(g, c, r); }
        private void DrawNeedle_CompassNorth(Graphics g, Rectangle rect) { float r = rect.Width * 0.5f; var c = new PointF(rect.Left + r, rect.Top + r); float a = MapValueToAngle(Value); var p = GetPerpendicularVector(a); float s = r * ClassicNeedleBaseWidthMultiplier / 2; var t = PointOnCircle(c, r * NeedleTipMultiplier, Value); var tail = PointOnCircle(c, r * NeedleTailMultiplier, Value, 180); var p1 = new PointF(c.X - p.X * s, c.Y - p.Y * s); var p2 = new PointF(c.X + p.X * s, c.Y + p.Y * s); using (var path = new GraphicsPath()) { path.AddPolygon(new[] { t, p2, tail, p1 }); using (var region1 = new Region(path)) { var halfPath = new GraphicsPath(); halfPath.AddPolygon(new[] { t, p2, c, p1 }); using (var region2 = new Region(halfPath)) { g.FillRegion(new SolidBrush(Color.FromArgb(NeedleTransparency, NeedleColor2)), region2); } } using (var pen = new Pen(Color.FromArgb(NeedleTransparency, NeedleColor1), 2f)) { DrawNeedleShadowAndFill(g, pen, path, a, r); } } DrawNeedleHub(g, c, r); }
        private void DrawNeedle_AnchorTail(Graphics g, Rectangle rect) { float r = rect.Width * 0.5f; var c = new PointF(rect.Left + r, rect.Top + r); float a = MapValueToAngle(Value); DrawNeedle_SimplePin(g, rect); float ts = r * KiteTailSizeMultiplier * 1.5f; var tb = PointOnCircle(c, r * NeedleTailMultiplier, Value, 180); var te = PointOnCircle(tb, ts * 0.5f, Value, 180); var p1 = PointOnCircle(te, ts, Value, 180 + 90); var p2 = PointOnCircle(te, ts, Value, 180 - 90); using (var path = new GraphicsPath()) { path.AddLine(tb, te); path.AddLine(p1, p2); g.DrawPath(new Pen(Color.FromArgb(NeedleTransparency, NeedleColor1), 4f) { StartCap = LineCap.RoundAnchor, EndCap = LineCap.RoundAnchor }, path); } DrawNeedleHub(g, c, r); }
        private void DrawNeedle_SciFiHollow(Graphics g, Rectangle rect) { float r = rect.Width * 0.5f; var c = new PointF(rect.Left + r, rect.Top + r); float a = MapValueToAngle(Value); var p = GetPerpendicularVector(a); float bw = r * BlockyNeedleWidthMultiplier; var t = PointOnCircle(c, r * NeedleTipMultiplier, Value); var bl = new PointF(c.X - p.X * bw / 2, c.Y - p.Y * bw / 2); var br = new PointF(c.X + p.X * bw / 2, c.Y + p.Y * bw / 2); using (var path = new GraphicsPath()) { path.AddPolygon(new[] { t, br, bl }); DrawNeedleShadowAndFill(g, new Pen(Color.FromArgb(NeedleTransparency, NeedleColor1), 2f), path, a, r); } DrawNeedleHub(g, c, r); }
        #endregion

        #region --- Helper Methods ---
        private float MapValueToAngle(float v) { float r = MaxValue - MinValue; return r <= 0 ? ScaleStartAngle : ScaleStartAngle + (Clamp(v, MinValue, MaxValue) - MinValue) / r * ScaleSweepAngle; }
        private PointF PointOnCircle(PointF c, float r, float v) { float a = MapValueToAngle(v); double rad = DegToRad(a); return new PointF(c.X + (float)Math.Cos(rad) * r, c.Y + (float)Math.Sin(rad) * r); }
        private PointF PointOnCircle(PointF c, float r, float v, float angleOffset) { float a = MapValueToAngle(v) + angleOffset; double rad = DegToRad(a); return new PointF(c.X + (float)Math.Cos(rad) * r, c.Y + (float)Math.Sin(rad) * r); }
        private static float Clamp(float v, float min, float max) => Math.Max(min, Math.Min(max, v));
        private float SpanForRange(float from, float to) { float t = MaxValue - MinValue; return t <= 0f ? 0f : (Clamp(to, MinValue, MaxValue) - Clamp(from, MinValue, MaxValue)) / t * ScaleSweepAngle; }
        private static double DegToRad(float deg) => (Math.PI / 180.0) * deg;
        private static int NiceNumber(float raw) { if (raw <= 0f) return 1; double exp = Math.Floor(Math.Log10(raw)), p = Math.Pow(10.0, exp), n = raw / p, nf; if (n <= 1.0) nf = 1.0; else if (n <= 2.0) nf = 2.0; else if (n <= 5.0) nf = 5.0; else nf = 10.0; return Math.Max(1, (int)(nf * p)); }
        private PointF GetDirectionVector(float a) { double rad = DegToRad(a); return new PointF((float)Math.Cos(rad), (float)Math.Sin(rad)); }
        private PointF GetPerpendicularVector(float a) { double rad = DegToRad(a); return new PointF(-(float)Math.Sin(rad), (float)Math.Cos(rad)); }
        #endregion
    }
}