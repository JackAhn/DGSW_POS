using DGSW_POS.View;

namespace DGSW_POS
{
    public partial class FoodButton
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.namelabel = new System.Windows.Forms.Label();
            this.pricelabel = new System.Windows.Forms.Label();
            this.picturebtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // namelabel
            // 
            this.namelabel.AutoSize = true;
            this.namelabel.BackColor = System.Drawing.Color.Lime;
            this.namelabel.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.namelabel.Location = new System.Drawing.Point(6, 7);
            this.namelabel.Name = "namelabel";
            this.namelabel.Size = new System.Drawing.Size(55, 16);
            this.namelabel.TabIndex = 2;
            this.namelabel.Text = "label1";
            // 
            // pricelabel
            // 
            this.pricelabel.AutoSize = true;
            this.pricelabel.BackColor = System.Drawing.Color.Lime;
            this.pricelabel.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.pricelabel.Location = new System.Drawing.Point(132, 89);
            this.pricelabel.Name = "pricelabel";
            this.pricelabel.Size = new System.Drawing.Size(55, 16);
            this.pricelabel.TabIndex = 2;
            this.pricelabel.Text = "label1";
            this.pricelabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picturebtn
            // 
            this.picturebtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picturebtn.Location = new System.Drawing.Point(0, 0);
            this.picturebtn.Name = "picturebtn";
            this.picturebtn.Size = new System.Drawing.Size(193, 109);
            this.picturebtn.TabIndex = 3;
            this.picturebtn.UseVisualStyleBackColor = true;
            // 
            // FoodButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.pricelabel);
            this.Controls.Add(this.namelabel);
            this.Controls.Add(this.picturebtn);
            this.Name = "FoodButton";
            this.Size = new System.Drawing.Size(193, 109);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Label namelabel;
        public System.Windows.Forms.Label pricelabel;
        public System.Windows.Forms.Button picturebtn;
    }
}
