using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WinFormShogi
{
    public static class TurnManage
    {
        public static int turn = 0;   //手番　プレイヤー：0    コンピュータ：1     プレイヤー選択中：2
        static Random random = new Random();


        public static void Sengo(Control control)
        {
            turn = random.Next(2);
            if (turn == 0)
            {
                control.Text = "手番：あなた";
                MessageBox.Show("あなたが先手です");
            }
            else if (turn == 1)
            {
                control.Text = "手番：コンピュータ";
                MessageBox.Show("あなたは後手です");
            }
        }

        public static void Turn(List<Piece> playerPieces, List<Piece> comPieces)
        {
            //プレイヤー手番の時
            if (turn == 0)
            {
                foreach (var playerPiece in playerPieces)
                {
                    playerPiece.eventMaking();
                }
                foreach (var comPiece in comPieces)
                {
                    comPiece.eventSuspend();
                }
            }
            //COM手番の時
            else if (turn == 1)
            {
                foreach (var playerPiece in playerPieces)
                {
                    playerPiece.eventSuspend();
                }
                foreach (var comPiece in comPieces)
                {
                    comPiece.eventMaking();
                }
            }

        }
    }
}

//１つ駒が選択されていたら、他の駒は選択できない
//if (playerPieces.Any(n => n.Onclick == 1))
//{
//    playerPieces.Where(n => n.Onclick == 0).ToList().ForEach(n => n.eventSuspend());
//}
//else
//{
//    foreach (var playerPiece in playerPieces)
//    {
//        playerPiece.eventMaking();
//    }
//}
