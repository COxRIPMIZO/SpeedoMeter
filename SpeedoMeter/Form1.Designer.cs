namespace SpeedoMeter
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.speedometerControl1 = new SpeedoMeter.SpeedometerControl();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // speedometerControl1
            // 
            this.speedometerControl1.ArrowheadSizeMultiplier = 0.1F;
            this.speedometerControl1.BackColor = System.Drawing.Color.White;
            this.speedometerControl1.BezelColor1 = System.Drawing.Color.White;
            this.speedometerControl1.BezelColor2 = System.Drawing.Color.White;
            this.speedometerControl1.BezelHighlightColor = System.Drawing.Color.White;
            this.speedometerControl1.BezelShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))));
            this.speedometerControl1.BezelWidthMultiplier = 0.08F;
            this.speedometerControl1.BlockyNeedleWidthMultiplier = 0.1F;
            this.speedometerControl1.ClassicNeedleBaseWidthMultiplier = 0.15F;
            this.speedometerControl1.DesiredTickLabelCount = 8;
            this.speedometerControl1.DialColor1 = System.Drawing.Color.White;
            this.speedometerControl1.DialColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.speedometerControl1.DialPadding = 1F;
            this.speedometerControl1.DualLineGapMultiplier = 0.02F;
            this.speedometerControl1.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.speedometerControl1.GaugePadding = 10;
            this.speedometerControl1.GlassEffectCenterColor = System.Drawing.Color.Transparent;
            this.speedometerControl1.GlassEffectOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.speedometerControl1.GlassEffectRect = ((System.Drawing.RectangleF)(resources.GetObject("speedometerControl1.GlassEffectRect")));
            this.speedometerControl1.HubColor1 = System.Drawing.Color.Black;
            this.speedometerControl1.HubColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.speedometerControl1.HubHighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.speedometerControl1.HubMinSize = 10F;
            this.speedometerControl1.HubShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.speedometerControl1.HubSizeMultiplier = 0.13F;
            this.speedometerControl1.IndicatorHubColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.speedometerControl1.IndicatorHubHighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.speedometerControl1.IndicatorTipLengthMultiplier = 0.1F;
            this.speedometerControl1.IndicatorTipWidthMultiplier = 0.05F;
            this.speedometerControl1.InnerShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.speedometerControl1.KiteTailSizeMultiplier = 0.1F;
            this.speedometerControl1.Location = new System.Drawing.Point(238, 31);
            this.speedometerControl1.MajorTickColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.speedometerControl1.MajorTickLengthMultiplier = 0.09F;
            this.speedometerControl1.MajorTickStep = 50;
            this.speedometerControl1.MajorTickWidth = 5F;
            this.speedometerControl1.MaxValue = 450F;
            this.speedometerControl1.MinorTickColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.speedometerControl1.MinorTickLengthMultiplier = 0.05F;
            this.speedometerControl1.MinorTicksPerMajor = 5;
            this.speedometerControl1.MinorTickWidth = 2F;
            this.speedometerControl1.MinValue = 0F;
            this.speedometerControl1.Name = "speedometerControl1";
            this.speedometerControl1.NeedleColor1 = System.Drawing.Color.Red;
            this.speedometerControl1.NeedleColor2 = System.Drawing.Color.Red;
            this.speedometerControl1.NeedleHighlightColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.speedometerControl1.NeedleHighlightColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.speedometerControl1.NeedleHighlightWidth = 2F;
            this.speedometerControl1.NeedleShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.speedometerControl1.NeedleShadowOffsetMultiplier = 0.02F;
            this.speedometerControl1.NeedleShoulderDistanceMultiplier = 0.08F;
            this.speedometerControl1.NeedleShoulderWidthMultiplier = 0.055F;
            this.speedometerControl1.NeedleStyle = SpeedoMeter.NeedleStyle.ModernThin;
            this.speedometerControl1.NeedleTailMultiplier = 0.2F;
            this.speedometerControl1.NeedleTailSplitDistanceMultiplier = 0.24F;
            this.speedometerControl1.NeedleTailWidthMultiplier = 0.03F;
            this.speedometerControl1.NeedleTipMultiplier = 0.75F;
            this.speedometerControl1.NeedleTransparency = 180;
            this.speedometerControl1.OrnateCircleTailRadiusMultiplier = 0.08F;
            this.speedometerControl1.OrnateDiamondSizeMultiplier = 0.1F;
            this.speedometerControl1.OuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.speedometerControl1.OuterBorderWidth = 1.5F;
            this.speedometerControl1.OuterShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.speedometerControl1.ScaleStartAngle = 135F;
            this.speedometerControl1.ScaleSweepAngle = 270F;
            this.speedometerControl1.Size = new System.Drawing.Size(668, 589);
            this.speedometerControl1.TabIndex = 0;
            this.speedometerControl1.TextShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.speedometerControl1.TickLabelColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.speedometerControl1.TickLabelFontFamily = "Calibri";
            this.speedometerControl1.TickLabelFontSizeMultiplier = 0.07F;
            this.speedometerControl1.TickLabelMinFontSize = 10F;
            this.speedometerControl1.TickLabelSpacing = 7F;
            this.speedometerControl1.TickZonePadding = 5F;
            this.speedometerControl1.UnitFontSizeScale = 0.4F;
            this.speedometerControl1.UnitLabel = "M/min";
            this.speedometerControl1.UnitMinFontSize = 10F;
            this.speedometerControl1.UnitTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.speedometerControl1.Value = 0F;
            this.speedometerControl1.ValueFontFamily = "Calibri";
            this.speedometerControl1.ValueFontSizeMultiplier = 0.1F;
            this.speedometerControl1.ValueMinFontSize = 18F;
            this.speedometerControl1.ValuePosition = ((System.Drawing.PointF)(resources.GetObject("speedometerControl1.ValuePosition")));
            this.speedometerControl1.ValueTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.speedometerControl1.ValueUnitVerticalSpacing = -12F;
            this.speedometerControl1.ZoneGreenColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(83)))));
            this.speedometerControl1.ZoneGreenEndValue = 100F;
            this.speedometerControl1.ZoneMinThickness = 2F;
            this.speedometerControl1.ZoneRedColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.speedometerControl1.ZoneRedStartValue = 250F;
            this.speedometerControl1.ZoneThicknessMultiplier = 0.02F;
            this.speedometerControl1.ZoneYellowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1223, 660);
            this.Controls.Add(this.speedometerControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private SpeedometerControl speedometerControl1;
    }
}

