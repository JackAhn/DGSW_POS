using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DGSW_POS.Model;
using DGSW_POS.Controller;
using System.Windows.Forms;

namespace DGSW_POS.View
{
    public partial class Checkorder : Form
    {
        FModel model; //생성자에서 받을 FModel 저장할 변수
        Menulist activeForm; //생성자에서 받을 Menulist 폼 저장할 변수
        int arrayct = 0;
        string buytype = "";

        public Checkorder(Menulist activeForm, FModel model, string buytype, int arrayct)
        {
            InitializeComponent();
            //주문했던 데이터 라벨에 삽입
            string ordertext = "";
            int orderprice = 0;
            this.buytype = buytype; //선택한 결제방식 저장
            for(int i = 0; i < model.TData.Count; i++)
            {
                ordertext += model.TData[i].FName + " 수량 : " + model.TData[i].FCount+"\n";
                orderprice += model.TData[i].FPrice;
            }
            ordertext += "총금액 : " + orderprice + "원 (" + buytype + ")";
            this.label2.Text = ordertext;
            this.model = model; //테이블에서 주문한 데이터 객체 저장
            this.activeForm = activeForm; //열려 있는 메뉴선택 폼 객체 저장
            this.arrayct = arrayct;
            this.CenterToScreen();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("결제가 완료되었습니다.", "결제 완료", MessageBoxButtons.OK, MessageBoxIcon.Information); //선택하지 않았으면 에러 메시지
            Main.af.InsertBuydata(buytype,  model);
            model = null; // 데이터 초기화
            AForm.fm[arrayct] = model; //배열에 초기화한 데이터 삽입
            this.Close();
            activeForm.Close(); //켜져 있는 메뉴폼 닫기
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
