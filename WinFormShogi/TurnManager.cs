using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormShogi
{
    public class turnManager
    {
        public static Turn turn = Turn.PLAYERTURN;
        public static int handlingCount = 0;
        public static int preCount = 0;
        static Random random = new Random();
        public Form1 form1;

        public turnManager(Form1 form1)
        {
            this.form1 = form1;
        }

        //先後決定
        public static void TurnShuffle(Control turnText)
        {
            turn = (Turn)random.Next(2);

            if (turn == Turn.PLAYERTURN)
            {
                turnText.Text = "手番：あなた";
                MessageBox.Show("あなたが先手です");
            }
            else if (turn == Turn.COMTURN)
            {
                turnText.Text = "手番：コンピュータ";
                MessageBox.Show("あなたは後手です");
            }
        }

        //待機（考慮中）時
        public async void RoundTurn(List<Piece> playerPieces, List<Piece> comPieces, List<Piece> emptyPieces,
                                    Control turnText, Control countText,
                                    ListBox playerList, ListBox comList, ListBox emptyList, ListBox playerSubList, ListBox comSubList,
                                    List<Piece> playerSubPieces, List<Piece> comSubPieces)
        {
            TurnStart(playerPieces, comPieces, emptyPieces, playerSubPieces, comSubPieces);

            while (true)
            {
                await Task.Delay(1 * 100);

                if (form1.playerSubPieces.Any(n => n.Name == "王将") || (form1.c_time_sec == 0 && form1.c_time_min == 0))
                {
                    MessageBox.Show("あなたの勝ちです");
                    break;
                }
                else if (form1.comSubPieces.Any(n => n.Name == "王将") || (form1.p_time_sec == 0 && form1.p_time_min == 0))
                {
                    MessageBox.Show("あなたの負けです");
                    break;
                }

                //手番カウントが進めば手番変更処理
                if (handlingCount > preCount)
                {
                    TurnStart(playerPieces, comPieces, emptyPieces, playerSubPieces, comSubPieces);
                    preCount = handlingCount;

                    form1.Invoke((Action)(() =>
                    {
                        countText.Text = $"手数：{preCount}";
                        if (turn == Turn.PLAYERTURN)
                        {
                            turnText.Text = "手番：あなた";
                        }
                        else if (turn == Turn.COMTURN)
                        {
                            turnText.Text = "手番：コンピュータ";
                        }

                        foreach (var item in playerPieces)
                        {
                            playerList.Items.Clear();
                        }
                        foreach (var item in comPieces)
                        {
                            comList.Items.Clear();
                        }
                        foreach (var item in emptyPieces)
                        {
                            emptyList.Items.Clear();
                        }
                        foreach (var item in playerSubPieces)
                        {
                            playerSubList.Items.Clear();
                        }
                        foreach (var item in comSubPieces)
                        {
                            comSubList.Items.Clear();
                        }

                        foreach (var item in playerPieces)
                        {
                            playerList.Items.Add(item.Name);
                        }
                        foreach (var item in comPieces)
                        {
                            comList.Items.Add(item.Name);
                        }
                        foreach (var item in emptyPieces)
                        {
                            emptyList.Items.Add(item.Name);
                        }
                        foreach (var item in playerSubPieces)
                        {
                            playerSubList.Items.Add(item.Name);
                        }
                        foreach (var item in comSubPieces)
                        {
                            comSubList.Items.Add(item.Name);
                        }
                    }));
                }
            }
        }


        //手番変更処理
        public static void TurnStart(List<Piece> playerPieces, List<Piece> comPieces, List<Piece> emptyPieces, List<Piece> playerSubPieces, List<Piece> comSubPieces)
        {
            //プレイヤー手番の時
            if (turn == Turn.PLAYERTURN)
            {
                foreach (var playerPiece in playerPieces)
                {
                    playerPiece.AttackPieceEvent();
                }
                foreach (var comPiece in comPieces)
                {
                    comPiece.DifenseOnBoardEvent();
                }
                foreach (var emptyPiece in emptyPieces)
                {
                    emptyPiece.DifenseOnBoardEvent();
                }
                foreach (var playerSubPiece in playerSubPieces)
                {
                    playerSubPiece.AttackPieceEvent();
                }
                foreach (var comSubPiece in comSubPieces)
                {
                    comSubPiece.DifenseOutBoardEvent();
                }
            }
            else if (turn == Turn.COMTURN)//COM手番の時
            {
                foreach (var playerPiece in playerPieces)
                {
                    playerPiece.DifenseOnBoardEvent();
                }
                foreach (var comPiece in comPieces)
                {
                    comPiece.AttackPieceEvent();
                }
                foreach (var emptyPiece in emptyPieces)
                {
                    emptyPiece.DifenseOnBoardEvent();
                }
                foreach (var playerSubPiece in playerSubPieces)
                {
                    playerSubPiece.DifenseOutBoardEvent();
                }
                foreach (var comSubPiece in comSubPieces)
                {
                    comSubPiece.AttackPieceEvent();
                }
            }
        }
    }
}
