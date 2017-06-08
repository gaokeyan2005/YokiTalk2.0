using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Yoki.View.UserControl
{
    public partial class ucEvaluate : System.Windows.Forms.UserControl
    {
        private Dictionary<int, int> scores = new Dictionary<int, int>();
        public ucEvaluate()
        {
            InitializeComponent();

            Business.AccountController.Instance.Data.OnEvaluateProtoChanged += (ev) =>
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    this.InitContent();
                });
            };

            InitContent();

            for (int i = 0; i < this.Controls.Count; i++)
			{
                ucEvaluateCell item = this.Controls[i] as ucEvaluateCell;
                item.OnResized += (o, e) =>
                {
                    int height = 0;
                    foreach (ucEvaluateCell cell in this.Controls)
                    {
                        height += cell.Height;
                    }
                    this.Height = height;
                };
                item.OnSelectedChanged += (o, e) =>
                {
                    scores[e.Key] = e.SelectedIndex + 1;
                };
			}
        }

        private void InitContent()
        {

            if (Business.AccountController.Instance.Data != null && Business.AccountController.Instance.Data.EvaluateProto != null && Business.AccountController.Instance.Data.EvaluateProto.Length == 5)
            {
                this.ucEvaluateCell1.Key = Business.AccountController.Instance.Data.EvaluateProto[0].ID;
                this.ucEvaluateCell1.Title = Business.AccountController.Instance.Data.EvaluateProto[0].Name;
                this.ucEvaluateCell1.Strs = (from s in Business.AccountController.Instance.Data.EvaluateProto[0].Items
                                             select s.Value.Text).ToArray();

                this.ucEvaluateCell2.Key = Business.AccountController.Instance.Data.EvaluateProto[1].ID;
                this.ucEvaluateCell2.Title = Business.AccountController.Instance.Data.EvaluateProto[1].Name;
                this.ucEvaluateCell2.Strs = (from s in Business.AccountController.Instance.Data.EvaluateProto[1].Items
                                             select s.Value.Text).ToArray();

                this.ucEvaluateCell3.Key = Business.AccountController.Instance.Data.EvaluateProto[2].ID;
                this.ucEvaluateCell3.Title = Business.AccountController.Instance.Data.EvaluateProto[2].Name;
                this.ucEvaluateCell3.Strs = (from s in Business.AccountController.Instance.Data.EvaluateProto[2].Items
                                             select s.Value.Text).ToArray();

                this.ucEvaluateCell4.Key = Business.AccountController.Instance.Data.EvaluateProto[3].ID;
                this.ucEvaluateCell4.Title = Business.AccountController.Instance.Data.EvaluateProto[3].Name;
                this.ucEvaluateCell4.Strs = (from s in Business.AccountController.Instance.Data.EvaluateProto[3].Items
                                             select s.Value.Text).ToArray();

                this.ucEvaluateCell5.Key = Business.AccountController.Instance.Data.EvaluateProto[4].ID;
                this.ucEvaluateCell5.Title = Business.AccountController.Instance.Data.EvaluateProto[4].Name;
                this.ucEvaluateCell5.Strs = (from s in Business.AccountController.Instance.Data.EvaluateProto[4].Items
                                             select s.Value.Text).ToArray();


                foreach (var ep in Business.AccountController.Instance.Data.EvaluateProto)
                {
                    this.scores.Add(ep.ID, -1);
                }
            }
        }

        public Dictionary<int, int> Scores
        {
            get
            {
                if (IsValidated)
                {
                    return this.scores;
                }
                else
                {
                    return null;
                }
            }
        }

        public bool IsValidated
        {
            get
            {
                foreach (var s in scores.Values)
                {
                    if (s <0 || s> 5)
                    {
                        return false;
                    }
                }
                return true;
            }
        }



    }
}
