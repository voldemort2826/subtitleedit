using Nikse.SubtitleEdit.Logic;
using System.Windows.Forms;
using System.ComponentModel;

namespace Nikse.SubtitleEdit.Forms.AudioToText
{
    public sealed partial class PostProcessingSettings : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AddPeriods { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool MergeLines { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool SplitLines { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool FixCasing { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool FixShortDuration { get; set; }

        public PostProcessingSettings()
        {
            UiUtil.PreInitialize(this);
            InitializeComponent();
            UiUtil.FixFonts(this);

            Text = LanguageSettings.Current.Settings.Title;

            checkBoxMergeShortLines.Text = LanguageSettings.Current.MergedShortLines.Title;
            checkBoxFixCasing.Text = LanguageSettings.Current.AudioToText.FixCasing;
            checkBoxAddPeriods.Text = LanguageSettings.Current.AudioToText.AddPeriods;
            checkBoxFixShortDuration.Text = LanguageSettings.Current.AudioToText.FixShortDuration;
            checkBoxSplitLongLines.Text = LanguageSettings.Current.SplitLongLines.Title;
            buttonOK.Text = LanguageSettings.Current.General.Ok;
            buttonCancel.Text = LanguageSettings.Current.General.Cancel;
        }

        private void PostProcessingSettings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        private void buttonOK_Click(object sender, System.EventArgs e)
        {
            AddPeriods = checkBoxAddPeriods.Checked;
            MergeLines = checkBoxMergeShortLines.Checked;
            SplitLines = checkBoxSplitLongLines.Checked;
            FixCasing = checkBoxFixCasing.Checked;
            FixShortDuration = checkBoxFixShortDuration.Checked;

            DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void PostProcessingSettings_Shown(object sender, System.EventArgs e)
        {
            checkBoxAddPeriods.Checked = AddPeriods;
            checkBoxMergeShortLines.Checked = MergeLines;
            checkBoxSplitLongLines.Checked = SplitLines;
            checkBoxFixCasing.Checked = FixCasing;
            checkBoxFixShortDuration.Checked = FixShortDuration;
        }
    }
}
