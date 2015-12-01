using System;
using System.Drawing;
using System.Windows.Forms;

namespace Quail
{
    public partial class OptionsForm : Form
  {
    private HighlightItem[] mHighlightItems = null;
    public HighlightItem[] HighlightItems
    {
      get { return mHighlightItems; }
      set { mHighlightItems = value; }
    }

    public OptionsForm()
    {
      InitializeComponent();
    }


    private void OptionsForm_Load(object sender, EventArgs e)
    {
      int row = 0;

      foreach (HighlightItem item in HighlightItems)
      {
        // Add label
        Label myLabel = new Label();
        myLabel.Text = item.ContainsString;
        myLabel.Width = 65;
        tableLayoutPanel1.Controls.Add(myLabel, 0, row);

        // Add color combo
        TextBox myTextBox = new TextBox();
        myTextBox.Text = item.HiglightColor;
        myTextBox.Name = "tbColor" + row.ToString();
        myTextBox.Width = 90;
        tableLayoutPanel1.Controls.Add(myTextBox, 1, row);

        // Add color picker button
        Button myButton = new Button();
        myButton.Text = "...";
        myButton.Tag = myTextBox.Name;
        myButton.Width = 35;
        myButton.Height = 23;
        myButton.Click += new EventHandler(btnGetColor_Click);
        tableLayoutPanel1.Controls.Add(myButton, 2, row);

        row++;
      }

    }

    private void btnDarkTheme_Click(object sender, EventArgs e)
    {
      this.tbBackgroundColor.Text = "Black";
      this.tbDefaultColor.Text = "White";
      this.tbFilterColor.Text = "Yellow";

      for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
      {
        Label myLabel = tableLayoutPanel1.Controls[i] as Label;
        if (myLabel != null)
        {
          TextBox myTextBox = tableLayoutPanel1.Controls[++i] as TextBox;
          switch (myLabel.Text)
          {
            case "\"DEBUG":
              myTextBox.Text = "White";
              break;
            case "\"INFO":
              myTextBox.Text = "Lime";
              break;
            case "\"WARNING":
              myTextBox.Text = "Coral";
              break;
            case "\"ERROR":
              myTextBox.Text = "Orangered";
              break;
            case "\"FATAL":
              myTextBox.Text = "Red";
              break;
            case "***":
              myTextBox.Text = "Red";
              break;
            default:
              myTextBox.Text = "White";
              break;
          }
        }
      }
    }

    private void btnLightTheme_Click(object sender, EventArgs e)
    {
      this.tbBackgroundColor.Text = "White";
      this.tbDefaultColor.Text = "Black";
      this.tbFilterColor.Text = "Navy";

      for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
      {
        Label myLabel = tableLayoutPanel1.Controls[i] as Label;
        if (myLabel != null)
        {
          TextBox myTextBox = tableLayoutPanel1.Controls[++i] as TextBox;
          switch (myLabel.Text)
          {
            case "\"DEBUG":
              myTextBox.Text = "Black";
              break;
            case "\"INFO":
              myTextBox.Text = "Gray";
              break;
            case "\"WARNING":
              myTextBox.Text = "Coral";
              break;
            case "\"ERROR":
              myTextBox.Text = "Maroon";
              break;
            case "\"FATAL":
              myTextBox.Text = "Red";
              break;
            case "***":
              myTextBox.Text = "Red";
              break;
            default:
              myTextBox.Text = "Black";
              break;
          }
        }
      }
    }

    private void btnGetColor_Click(object sender, EventArgs e)
    {
      if (this.colorDialog1.ShowDialog() == DialogResult.OK)
      {
        Button myButton = sender as Button;
        TextBox myTextBox = Controls.Find(myButton.Tag.ToString(), true)[0] as TextBox;
        myTextBox.Text = ColorTranslator.ToHtml(colorDialog1.Color);
      }
    }

  }
}