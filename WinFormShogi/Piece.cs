using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Forms;

namespace WinFormShogi
{
    public class Piece : PictureBox
    {
        public string PicAdress { get; set; }
        public int Onclick { get; set; } = 0;
        public Fugou Fugou { get; set; }
        public Owner Owner { get; set; }
        public List<Fugou> CanMovePosList { get; set; } = new List<Fugou>();
        Form1 form1;
        List<Piece> judgeList = new List<Piece>();
        MessageBoxResult reverseSelect = new MessageBoxResult();

        public Piece(Form1 form1)
        {
            this.form1 = form1;
        }

        public void AttackPieceEvent()
        {
            this.Click += new EventHandler(ClickEvent);
            this.Click -= new EventHandler(MoveEvent);
        }

        public void DifenseOnBoardEvent()
        {
            this.Click -= new EventHandler(ClickEvent);
            this.Click += new EventHandler(MoveEvent);
        }

        public void DifenseOutBoardEvent()
        {
            this.Click -= new EventHandler(ClickEvent);
            this.Click -= new EventHandler(MoveEvent);
        }

        //駒クリックイベント
        //１度クリックされたら色変更、再度クリックで元に戻す
        public void ClickEvent(object sender, EventArgs e)
        {
            if (Onclick == 0)
            {
                form1.pieces.ForEach(x =>
                {
                    x.BackColor = Color.Transparent;
                    x.BorderStyle = BorderStyle.None;
                    x.Onclick = 0;
                });
                Onclick = 1;
                ChangeBoxOn(this);
            }

            else if (Onclick == 1)
            {
                Onclick = 0;
                ChangeBoxOff(this);
            }
        }

        //どれか駒がクリックされている状態で、他のマスをクリックしたときの処理
        public void MoveEvent(object sender, EventArgs e)
        {
            if (this.Owner == Owner.PLAYER)
            {
                bool canmove = CanMove(form1.playerPieces, form1.comPieces, form1.pieces);
                if (form1.comPieces.Any(n => n.Onclick == 1) && canmove)
                {
                    ShowReveaseWindow();
                    SelectReverse();

                    if (this.Owner != Owner.EMPTY)
                    {
                        OnHandyBox(Turn.COMTURN);
                    }
                    ChangePiece(form1.playerPieces, form1.comPieces);
                    ClearBox(form1.comPieces);
                    ChangeBoxOff(this);
                    turnManager.turn = Turn.PLAYERTURN;
                    turnManager.handlingCount++;
                }
            }

            else if (this.Owner == Owner.COMPUTER)
            {
                bool canmove = CanMove(form1.playerPieces, form1.comPieces, form1.pieces);
                if (form1.playerPieces.Any(n => n.Onclick == 1) && canmove)
                {
                    ShowReveaseWindow();
                    SelectReverse();

                    if (this.Owner != Owner.EMPTY)
                    {
                        OnHandyBox(Turn.PLAYERTURN);
                    }
                    ChangePiece(form1.playerPieces, form1.comPieces);
                    ClearBox(form1.playerPieces);
                    ChangeBoxOff(this);
                    turnManager.turn = Turn.COMTURN;
                    turnManager.handlingCount++;
                }
            }

            else if (this.Owner == Owner.EMPTY)
            {
                bool canmove = CanMove(form1.playerPieces, form1.comPieces, form1.pieces);
                if (form1.playerPieces.Any(n => n.Onclick == 1) && canmove)
                {
                    ShowReveaseWindow();
                    SelectReverse();

                    ChangePiece(form1.playerPieces, form1.comPieces);
                    ClearBox(form1.playerPieces);
                    ChangeBoxOff(this);
                    turnManager.turn = Turn.COMTURN;
                    turnManager.handlingCount++;
                }
                else if (form1.comPieces.Any(n => n.Onclick == 1) && canmove)
                {
                    ShowReveaseWindow();
                    SelectReverse();
                    ChangePiece(form1.playerPieces, form1.comPieces);
                    ClearBox(form1.comPieces);
                    ChangeBoxOff(this);
                    turnManager.turn = Turn.PLAYERTURN;
                    turnManager.handlingCount++;
                }
                else if (form1.playerSubPieces.Any(n => n.Onclick == 1) && CanDrop(form1.playerSubPieces))
                {
                    DropPiece(form1.playerSubPieces);
                    turnManager.turn = Turn.COMTURN;
                    turnManager.handlingCount++;
                }
                else if (form1.comSubPieces.Any(n => n.Onclick == 1) && CanDrop(form1.comSubPieces))
                {
                    DropPiece(form1.comSubPieces);
                    turnManager.turn = Turn.PLAYERTURN;
                    turnManager.handlingCount++;
                }
            }
        }

        //駒が打てるか判断
        public bool CanDrop(List<Piece> subPieces)
        {
            var temp = subPieces.FirstOrDefault(n => n.Onclick == 1);

            if (temp.Name == "歩兵")
            {
                if (subPieces == form1.playerSubPieces)
                {
                    return this.Fugou.Y >= 2 && !form1.playerPieces.Any(n => n.Fugou.X == this.Fugou.X && n.Name == "歩兵");
                }
                else if (subPieces == form1.comSubPieces)
                {
                    return this.Fugou.Y <= 8 && !form1.comPieces.Any(n => n.Fugou.X == this.Fugou.X && n.Name == "歩兵");
                }
            }
            else if (temp.Name == "香車")
            {
                if (subPieces == form1.playerSubPieces)
                {
                    return this.Fugou.Y >= 2;
                }
                else if (subPieces == form1.comSubPieces)
                {
                    return this.Fugou.Y <= 8;
                }

            }
            else if (temp.Name == "桂馬")
            {
                if (subPieces == form1.playerSubPieces)
                {
                    return this.Fugou.Y >= 3;
                }
                else if (subPieces == form1.comSubPieces)
                {
                    return this.Fugou.Y <= 7;
                }
            }
            return true;
        }

        //持ち駒を打つ
        public void DropPiece(List<Piece> subPieces)
        {
            var temp = new Piece(form1);
            temp = subPieces.FirstOrDefault(n => n.Onclick == 1);
            this.Image = temp.Image;
            this.Name = temp.Name;
            this.CanMovePosList = temp.CanMovePosList;
            this.Onclick = 0;
            if (turnManager.turn == Turn.PLAYERTURN)
            {
                this.Owner = Owner.PLAYER;
                form1.playerPieces.Add(this);
            }
            else if (turnManager.turn == Turn.COMTURN)
            {
                this.Owner = Owner.COMPUTER;
                form1.comPieces.Add(this);
            }
            form1.emptyPieces.Remove(this);
            subPieces.Remove(temp);
            temp.Dispose();
            DrawSubPiece(subPieces);
        }

        //マスの色を付ける
        public void ChangeBoxOn(Piece piece)
        {
            piece.BackColor = Color.Khaki;
            piece.BorderStyle = BorderStyle.FixedSingle;
        }

        //マスの色を消す
        public void ChangeBoxOff(Piece piece)
        {
            piece.BackColor = Color.Transparent;
            piece.BorderStyle = BorderStyle.None;
        }

        //駒インスタンスの内容を引用して入れ替える
        public void ChangePiece(List<Piece> playerPieces, List<Piece> comPieces)
        {
            if (turnManager.turn == Turn.PLAYERTURN)
            {
                var temp = new Piece(form1);
                temp = playerPieces.FirstOrDefault(n => n.Onclick == 1);
                this.Image = temp.Image;
                this.Name = temp.Name;
                this.CanMovePosList = temp.CanMovePosList;
                this.Owner = Owner.PLAYER;
                playerPieces.Add(this);
                if (form1.comPieces.Any(n => n == this))
                {
                    form1.comPieces.Remove(this);
                }
                else if (form1.emptyPieces.Any(n => n == this))
                {
                    form1.emptyPieces.Remove(this);
                }
            }
            else if (turnManager.turn == Turn.COMTURN)
            {
                var temp = new Piece(form1);
                temp = comPieces.FirstOrDefault(n => n.Onclick == 1);
                this.Image = temp.Image;
                this.Name = temp.Name;
                this.CanMovePosList = temp.CanMovePosList;
                this.Owner = Owner.COMPUTER;
                comPieces.Add(this);
                if (form1.playerPieces.Any(n => n == this))
                {
                    form1.playerPieces.Remove(this);
                }
                else if (form1.emptyPieces.Any(n => n == this))
                {
                    form1.emptyPieces.Remove(this);
                }
            }
        }

        //取った駒を駒台に乗せる
        public void OnHandyBox(Turn turn)
        {
            if (turn == Turn.PLAYERTURN)
            {
                MakeSubPiece(form1.playerSubPieces);
                DrawSubPiece(form1.playerSubPieces);
            }
            else if (turn == Turn.COMTURN)
            {
                MakeSubPiece(form1.comSubPieces);
                DrawSubPiece(form1.comSubPieces);
            }
        }

        //持ち駒生成
        public void MakeSubPiece(List<Piece> subPieces)
        {
            var piece = new Piece(form1);
            piece.Name = this.Name;
            if (subPieces == form1.playerSubPieces)
            {
                piece.Owner = Owner.PLAYER;
                form1.pieces.Add(piece);
                form1.playerSubPieces.Add(piece);
                form1.comPieces.Remove(this);
                form1.playerSubPieces.OrderBy(n => n.Name);

                if (piece.Name == "歩兵" || piece.Name == "と金")
                {
                    piece.CanMovePosList.Add(new Fugou(0, -1));
                    piece.Image = Image.FromFile(@"Pieces\fu.png");
                    piece.Name = "歩兵";
                }
                else if (piece.Name == "香車" || piece.Name == "成香")
                {
                    for (int i = -8; i <= -1; i++)
                    {
                        piece.CanMovePosList.Add(new Fugou(0, i));
                    }
                    piece.Image = Image.FromFile(@"Pieces\kyou.png");
                    piece.Name = "香車";
                }
                else if (piece.Name == "桂馬" || piece.Name == "成桂")
                {
                    piece.CanMovePosList.Add(new Fugou(1, -2));
                    piece.CanMovePosList.Add(new Fugou(-1, -2));
                    piece.Image = Image.FromFile(@"Pieces\kei.png");
                    piece.Name = "桂馬";
                }
                else if (piece.Name == "銀将" || piece.Name == "成銀")
                {
                    piece.CanMovePosList.Add(new Fugou(-1, -1));
                    piece.CanMovePosList.Add(new Fugou(0, -1));
                    piece.CanMovePosList.Add(new Fugou(1, -1));
                    piece.CanMovePosList.Add(new Fugou(1, 1));
                    piece.CanMovePosList.Add(new Fugou(-1, 1));
                    piece.Image = Image.FromFile(@"Pieces\gin.png");
                    piece.Name = "銀将";
                }
                else if (piece.Name == "金将")
                {
                    for (int i = -1; i <= 1; i++)
                    {
                        piece.CanMovePosList.Add(new Fugou(i, -1));
                        piece.CanMovePosList.Add(new Fugou(i, 0));
                    }
                    piece.CanMovePosList.Add(new Fugou(0, 1));
                    piece.Image = Image.FromFile(@"Pieces\kin.png");
                    piece.Name = "金将";
                }
                else if (piece.Name == "飛車" || piece.Name == "龍王")
                {
                    for (int i = -8; i <= 8; i++)
                    {
                        piece.CanMovePosList.Add(new Fugou(0, i));
                        piece.CanMovePosList.Add(new Fugou(i, 0));
                    }
                    piece.Image = Image.FromFile(@"Pieces\hisya.png");
                    piece.Name = "飛車";
                }
                else if (piece.Name == "角行" || piece.Name == "龍馬")
                {
                    for (int i = -8; i <= 8; i++)
                    {
                        piece.CanMovePosList.Add(new Fugou(i, i));
                        piece.CanMovePosList.Add(new Fugou(i, -i));
                    }
                    piece.Image = Image.FromFile(@"Pieces\kaku.png");
                    piece.Name = "角行";
                }
            }
            else if (subPieces == form1.comSubPieces)
            {
                piece.Owner = Owner.COMPUTER;
                form1.pieces.Add(piece);
                form1.comSubPieces.Add(piece);
                form1.playerPieces.Remove(this);
                form1.comSubPieces.OrderBy(n => n.Name);

                if (piece.Name == "歩兵" || piece.Name == "と金")
                {
                    piece.CanMovePosList.Add(new Fugou(0, 1));
                    piece.Image = Image.FromFile(@"Pieces\fu.png");
                    piece.Name = "歩兵";
                    piece.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                }
                else if (piece.Name == "香車" || piece.Name == "成香")
                {
                    for (int i = 1; i <= 8; i++)
                    {
                        piece.CanMovePosList.Add(new Fugou(0, i));
                    }
                    piece.Image = Image.FromFile(@"Pieces\kyou.png");
                    piece.Name = "香車";
                    piece.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                }
                else if (piece.Name == "桂馬" || piece.Name == "成桂")
                {
                    piece.CanMovePosList.Add(new Fugou(1, 2));
                    piece.CanMovePosList.Add(new Fugou(-1, 2));
                    piece.Image = Image.FromFile(@"Pieces\kei.png");
                    piece.Name = "桂馬";
                    piece.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                }
                else if (piece.Name == "銀将" || piece.Name == "成銀")
                {
                    piece.CanMovePosList.Add(new Fugou(-1, 1));
                    piece.CanMovePosList.Add(new Fugou(0, 1));
                    piece.CanMovePosList.Add(new Fugou(1, 1));
                    piece.CanMovePosList.Add(new Fugou(1, -1));
                    piece.CanMovePosList.Add(new Fugou(-1, -1));
                    piece.Image = Image.FromFile(@"Pieces\gin.png");
                    piece.Name = "銀将";
                    piece.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                }
                else if (piece.Name == "金将")
                {
                    for (int i = -1; i <= 1; i++)
                    {
                        piece.CanMovePosList.Add(new Fugou(i, 1));
                        piece.CanMovePosList.Add(new Fugou(i, 0));
                    }
                    piece.CanMovePosList.Add(new Fugou(0, -1));
                    piece.Image = Image.FromFile(@"Pieces\kin.png");
                    piece.Name = "金将";
                    piece.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                }
                else if (piece.Name == "飛車" || piece.Name == "龍王")
                {
                    for (int i = -8; i <= 8; i++)
                    {
                        piece.CanMovePosList.Add(new Fugou(0, i));
                        piece.CanMovePosList.Add(new Fugou(i, 0));
                    }
                    piece.Image = Image.FromFile(@"Pieces\hisya.png");
                    piece.Name = "飛車";
                    piece.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                }
                else if (piece.Name == "角行" || piece.Name == "龍馬")
                {
                    for (int i = -8; i <= 8; i++)
                    {
                        piece.CanMovePosList.Add(new Fugou(i, i));
                        piece.CanMovePosList.Add(new Fugou(i, -i));
                    }
                    piece.Image = Image.FromFile(@"Pieces\kaku.png");
                    piece.Name = "角行";
                    piece.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                }
            }
        }
        //持ち駒生成・描画
        public void DrawSubPiece(List<Piece> subPieces)
        {
            for (int i = 0; i < subPieces.Count; i++)
            {
                subPieces[i].Size = new Size(30, 35);
                subPieces[i].SizeMode = PictureBoxSizeMode.Zoom;
                subPieces[i].BackColor = Color.Transparent;
                subPieces[i].Onclick = 0;
                form1.Controls.Add(subPieces[i]);
                subPieces[i].BringToFront();
                if (subPieces == form1.playerSubPieces)
                {
                    form1.playerSubPieces[i].Location = new Point(420, 100 + (i * 35));
                }
                else if (subPieces == form1.comSubPieces)
                {
                    form1.comSubPieces[i].Location = new Point(10, 30 + (i * 35));
                }
            }
        }

        //移動前のマスをEMPTY化
        public void ClearBox(List<Piece> pieces)
        {
            var temp = new Piece(form1);
            temp = pieces.FirstOrDefault(n => n.Onclick == 1);
            temp.Image = null;
            temp.Name = null;
            temp.Owner = Owner.EMPTY;
            temp.CanMovePosList = null;
            temp.Onclick = 0;
            ChangeBoxOff(temp);
            form1.emptyPieces.Add(temp);
            pieces.Remove(temp);
        }

        //動ける場所か判断
        public bool CanMove(List<Piece> playerPieces, List<Piece> comPieces, List<Piece> pieces)
        {
            if (turnManager.turn == Turn.PLAYERTURN && playerPieces.Any(n => n.Onclick == 1))
            {
                var temp = new Piece(form1);
                temp = playerPieces.FirstOrDefault(n => n.Onclick == 1);
                var diff = this.Fugou - temp.Fugou;
                judgeList.Clear();

                //途中に駒があったら動けない
                if (temp.CanMovePosList.Any(n => n.X == diff.X && n.Y == diff.Y))
                {
                    if (temp.Name == "香車")
                    {
                        var tempList = pieces.Where(n => n.Fugou.X == temp.Fugou.X).ToList();
                        for (int i = 0; i < tempList.Count(); i++)
                        {
                            for (int m = this.Fugou.Y + 1; m <= temp.Fugou.Y - 1; m++)
                            {
                                if (tempList[i].Fugou.Y == m)
                                {
                                    judgeList.Add(tempList[i]);
                                }
                            }
                        }
                        if (judgeList.All(n => n.Owner == Owner.EMPTY)) return true;
                        return false;
                    }
                    else if (temp.Name == "飛車" || temp.Name == "龍王")
                    {
                        var tempListY = pieces.Where(n => n.Fugou.X == temp.Fugou.X).ToList();
                        var tempListX = pieces.Where(n => n.Fugou.Y == temp.Fugou.Y).ToList();
                        if (diff.X == 0)
                        {
                            for (int i = 0; i < tempListY.Count(); i++)
                            {
                                if (diff.Y < 0)
                                {
                                    for (int m = this.Fugou.Y + 1; m <= temp.Fugou.Y - 1; m++)
                                    {
                                        if (tempListY[i].Fugou.Y == m)
                                        {
                                            judgeList.Add(tempListY[i]);
                                        }
                                    }
                                }
                                else if (diff.Y > 0)
                                {
                                    for (int m = temp.Fugou.Y + 1; m <= this.Fugou.Y - 1; m++)
                                    {
                                        if (tempListY[i].Fugou.Y == m)
                                        {
                                            judgeList.Add(tempListY[i]);
                                        }
                                    }
                                }
                            }
                        }

                        else if (diff.Y == 0)
                        {
                            for (int i = 0; i < tempListX.Count(); i++)
                            {
                                if (diff.X < 0)
                                {
                                    for (int m = this.Fugou.X + 1; m <= temp.Fugou.X - 1; m++)
                                    {
                                        if (tempListX[i].Fugou.X == m)
                                        {
                                            judgeList.Add(tempListX[i]);
                                        }
                                    }
                                }
                                else if (diff.X > 0)
                                {
                                    for (int m = temp.Fugou.X + 1; m <= this.Fugou.X - 1; m++)
                                    {
                                        if (tempListX[i].Fugou.X == m)
                                        {
                                            judgeList.Add(tempListX[i]);
                                        }
                                    }
                                }
                            }
                        }
                        if (judgeList.All(n => n.Owner == Owner.EMPTY)) return true;
                        return false;
                    }
                    else if (temp.Name == "角行" || temp.Name == "龍馬")
                    {
                        for (int i = 0; i < pieces.Count(); i++)
                        {
                            if (diff.X == 1 && diff.Y == 1 || diff.X == -1 && diff.Y == -1 || diff.X == -1 && diff.Y == 1 || diff.X == 1 && diff.Y == -1)
                            {
                                return true;
                            }

                            else if (diff.X > 0 && diff.Y > 0)
                            {
                                for (int m = 1; m <= diff.X - 1; m++)
                                {
                                    if (pieces[i].Fugou.X == temp.Fugou.X + m && pieces[i].Fugou.Y == temp.Fugou.Y + m)
                                    {
                                        judgeList.Add(pieces[i]);
                                    }
                                }
                            }
                            else if (diff.X < 0 && diff.Y < 0)
                            {
                                for (int m = diff.X + 1; m <= -1; m++)
                                {
                                    if (pieces[i].Fugou.X == temp.Fugou.X + m && pieces[i].Fugou.Y == temp.Fugou.Y + m)
                                    {
                                        judgeList.Add(pieces[i]);
                                    }
                                }
                            }
                            else if (diff.X < 0 && diff.Y > 0)
                            {
                                for (int m = diff.X + 1; m <= -1; m++)
                                {
                                    if (pieces[i].Fugou.X == temp.Fugou.X + m && pieces[i].Fugou.Y == temp.Fugou.Y - m)
                                    {
                                        judgeList.Add(pieces[i]);
                                    }
                                }
                            }
                            else if (diff.X > 0 && diff.Y < 0)
                            {
                                for (int m = 1; m <= diff.X - 1; m++)
                                {
                                    if (pieces[i].Fugou.X == temp.Fugou.X + m && pieces[i].Fugou.Y == temp.Fugou.Y - m)
                                    {
                                        judgeList.Add(pieces[i]);
                                    }
                                }
                            }
                        }
                        if (judgeList.All(n => n.Owner == Owner.EMPTY)) return true;
                        return false;
                    }
                }
                return temp.CanMovePosList.Any(n => n.X == diff.X && n.Y == diff.Y);
            }
            else if (turnManager.turn == Turn.COMTURN && comPieces.Any(n => n.Onclick == 1))
            {
                var temp = new Piece(form1);
                temp = comPieces.FirstOrDefault(n => n.Onclick == 1);
                var diff = this.Fugou - temp.Fugou;
                judgeList.Clear();

                //途中に駒があったら動けない
                if (temp.CanMovePosList.Any(n => n.X == diff.X && n.Y == diff.Y))
                {
                    if (temp.Name == "香車")
                    {
                        var tempList = pieces.Where(n => n.Fugou.X == temp.Fugou.X).ToList();
                        for (int i = 0; i < tempList.Count(); i++)
                        {
                            for (int m = temp.Fugou.Y + 1; m <= this.Fugou.Y - 1; m++)
                            {
                                if (tempList[i].Fugou.Y == m)
                                {
                                    judgeList.Add(tempList[i]);
                                }
                            }
                        }
                        if (judgeList.All(n => n.Owner == Owner.EMPTY)) return true;
                        return false;
                    }
                    else if (temp.Name == "飛車" || temp.Name == "龍王")
                    {
                        var tempListY = pieces.Where(n => n.Fugou.X == temp.Fugou.X).ToList();
                        var tempListX = pieces.Where(n => n.Fugou.Y == temp.Fugou.Y).ToList();
                        if (diff.X == 0)
                        {
                            for (int i = 0; i < tempListY.Count(); i++)
                            {
                                if (diff.Y < 0)
                                {
                                    for (int m = this.Fugou.Y + 1; m <= temp.Fugou.Y - 1; m++)
                                    {
                                        if (tempListY[i].Fugou.Y == m)
                                        {
                                            judgeList.Add(tempListY[i]);
                                        }
                                    }
                                }
                                else if (diff.Y > 0)
                                {
                                    for (int m = temp.Fugou.Y + 1; m <= this.Fugou.Y - 1; m++)
                                    {
                                        if (tempListY[i].Fugou.Y == m)
                                        {
                                            judgeList.Add(tempListY[i]);
                                        }
                                    }
                                }
                            }
                        }

                        else if (diff.Y == 0)
                        {
                            for (int i = 0; i < tempListX.Count(); i++)
                            {
                                if (diff.X < 0)
                                {
                                    for (int m = this.Fugou.X + 1; m <= temp.Fugou.X - 1; m++)
                                    {
                                        if (tempListX[i].Fugou.X == m)
                                        {
                                            judgeList.Add(tempListX[i]);
                                        }
                                    }
                                }
                                else if (diff.X > 0)
                                {
                                    for (int m = temp.Fugou.X + 1; m <= this.Fugou.X - 1; m++)
                                    {
                                        if (tempListX[i].Fugou.X == m)
                                        {
                                            judgeList.Add(tempListX[i]);
                                        }
                                    }
                                }
                            }
                        }
                        if (judgeList.All(n => n.Owner == Owner.EMPTY)) return true;
                        return false;
                    }
                    else if (temp.Name == "角行" || temp.Name == "龍馬")
                    {
                        for (int i = 0; i < pieces.Count(); i++)
                        {
                            if (diff.X == 1 && diff.Y == 1 || diff.X == -1 && diff.Y == -1 || diff.X == -1 && diff.Y == 1 || diff.X == 1 && diff.Y == -1)
                            {
                                return true;
                            }

                            else if (diff.X > 0 && diff.Y > 0)
                            {
                                for (int m = 1; m <= diff.X - 1; m++)
                                {
                                    if (pieces[i].Fugou.X == temp.Fugou.X + m && pieces[i].Fugou.Y == temp.Fugou.Y + m)
                                    {
                                        judgeList.Add(pieces[i]);
                                    }
                                }
                            }
                            else if (diff.X < 0 && diff.Y < 0)
                            {
                                for (int m = diff.X + 1; m <= -1; m++)
                                {
                                    if (pieces[i].Fugou.X == temp.Fugou.X + m && pieces[i].Fugou.Y == temp.Fugou.Y + m)
                                    {
                                        judgeList.Add(pieces[i]);
                                    }
                                }
                            }
                            else if (diff.X < 0 && diff.Y > 0)
                            {
                                for (int m = diff.X + 1; m <= -1; m++)
                                {
                                    if (pieces[i].Fugou.X == temp.Fugou.X + m && pieces[i].Fugou.Y == temp.Fugou.Y - m)
                                    {
                                        judgeList.Add(pieces[i]);
                                    }
                                }
                            }
                            else if (diff.X > 0 && diff.Y < 0)
                            {
                                for (int m = 1; m <= diff.X - 1; m++)
                                {
                                    if (pieces[i].Fugou.X == temp.Fugou.X + m && pieces[i].Fugou.Y == temp.Fugou.Y - m)
                                    {
                                        judgeList.Add(pieces[i]);
                                    }
                                }
                            }
                        }
                        if (judgeList.All(n => n.Owner == Owner.EMPTY)) return true;
                        return false;
                    }
                }

                return temp.CanMovePosList.Any(n => n.X == diff.X && n.Y == diff.Y);
            }
            else
            {
                return false;
            }
        }

        //成り選択・動作
        public void SelectReverse()
        {
            if (reverseSelect == MessageBoxResult.Yes)
            {
                // 「はい」ボタンを押した場合の処理
                if (turnManager.turn == Turn.PLAYERTURN)
                {
                    foreach (var item in form1.playerPieces)
                    {
                        if (item.Name == "歩兵" && item.Onclick == 1)
                        {
                            item.Image = Image.FromFile(@"Pieces\tokin.png");
                            item.Name = "と金";
                            item.CanMovePosList.Clear();
                            for (int i = -1; i <= 1; i++)
                            {
                                item.CanMovePosList.Add(new Fugou(i, -1));
                                item.CanMovePosList.Add(new Fugou(i, 0));
                            }
                            item.CanMovePosList.Add(new Fugou(0, 1));
                        }
                        else if (item.Name == "香車" && item.Onclick == 1)
                        {
                            item.Image = Image.FromFile(@"Pieces\narikyou.png");
                            item.Name = "成香";
                            item.CanMovePosList.Clear();
                            for (int i = -1; i <= 1; i++)
                            {
                                item.CanMovePosList.Add(new Fugou(i, -1));
                                item.CanMovePosList.Add(new Fugou(i, 0));
                            }
                            item.CanMovePosList.Add(new Fugou(0, 1));
                        }
                        else if (item.Name == "桂馬" && item.Onclick == 1)
                        {
                            item.Image = Image.FromFile(@"Pieces\narikei.png");
                            item.Name = "成桂";
                            item.CanMovePosList.Clear();
                            for (int i = -1; i <= 1; i++)
                            {
                                item.CanMovePosList.Add(new Fugou(i, -1));
                                item.CanMovePosList.Add(new Fugou(i, 0));
                            }
                            item.CanMovePosList.Add(new Fugou(0, 1));
                        }
                        else if (item.Name == "銀将" && item.Onclick == 1)
                        {
                            item.Image = Image.FromFile(@"Pieces\narigin.png");
                            item.Name = "成銀";
                            item.CanMovePosList.Clear();
                            for (int i = -1; i <= 1; i++)
                            {
                                item.CanMovePosList.Add(new Fugou(i, -1));
                                item.CanMovePosList.Add(new Fugou(i, 0));
                            }
                            item.CanMovePosList.Add(new Fugou(0, 1));
                        }
                        else if (item.Name == "角行" && item.Onclick == 1)
                        {
                            item.Image = Image.FromFile(@"Pieces\uma.png");
                            item.Name = "龍馬";
                            item.CanMovePosList.Clear();
                            for (int i = -8; i <= 8; i++)
                            {
                                item.CanMovePosList.Add(new Fugou(i, i));
                                item.CanMovePosList.Add(new Fugou(i, -i));
                            }
                            item.CanMovePosList.Add(new Fugou(0, -1));
                            item.CanMovePosList.Add(new Fugou(0, 1));
                            item.CanMovePosList.Add(new Fugou(-1, 0));
                            item.CanMovePosList.Add(new Fugou(1, 0));
                        }
                        else if (item.Name == "飛車" && item.Onclick == 1)
                        {
                            item.Image = Image.FromFile(@"Pieces\ryu.png");
                            item.Name = "龍王";
                            item.CanMovePosList.Clear();
                            for (int i = -8; i <= 8; i++)
                            {
                                item.CanMovePosList.Add(new Fugou(0, i));
                                item.CanMovePosList.Add(new Fugou(i, 0));
                            }
                            item.CanMovePosList.Add(new Fugou(-1, -1));
                            item.CanMovePosList.Add(new Fugou(1, 1));
                            item.CanMovePosList.Add(new Fugou(-1, 1));
                            item.CanMovePosList.Add(new Fugou(1, -1));
                        }
                    }
                }
                else if (turnManager.turn == Turn.COMTURN)
                {
                    foreach (var item in form1.comPieces)
                    {
                        if (item.Name == "歩兵" && item.Onclick == 1)
                        {
                            item.Image = Image.FromFile(@"Pieces\tokin.png");
                            item.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                            item.Name = "と金";
                            item.CanMovePosList.Clear();
                            for (int i = -1; i <= 1; i++)
                            {
                                item.CanMovePosList.Add(new Fugou(i, 1));
                                item.CanMovePosList.Add(new Fugou(i, 0));
                            }
                            item.CanMovePosList.Add(new Fugou(0, -1));
                        }
                        else if (item.Name == "香車" && item.Onclick == 1)
                        {
                            item.Image = Image.FromFile(@"Pieces\narikyou.png");
                            item.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                            item.Name = "成香";
                            item.CanMovePosList.Clear();
                            for (int i = -1; i <= 1; i++)
                            {
                                item.CanMovePosList.Add(new Fugou(i, 1));
                                item.CanMovePosList.Add(new Fugou(i, 0));
                            }
                            item.CanMovePosList.Add(new Fugou(0, -1));
                        }
                        else if (item.Name == "桂馬" && item.Onclick == 1)
                        {
                            item.Image = Image.FromFile(@"Pieces\narikei.png");
                            item.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                            item.Name = "成桂";
                            item.CanMovePosList.Clear();
                            for (int i = -1; i <= 1; i++)
                            {
                                item.CanMovePosList.Add(new Fugou(i, 1));
                                item.CanMovePosList.Add(new Fugou(i, 0));
                            }
                            item.CanMovePosList.Add(new Fugou(0, -1));
                        }
                        else if (item.Name == "銀将" && item.Onclick == 1)
                        {
                            item.Image = Image.FromFile(@"Pieces\narigin.png");
                            item.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                            item.Name = "成銀";
                            item.CanMovePosList.Clear();
                            for (int i = -1; i <= 1; i++)
                            {
                                item.CanMovePosList.Add(new Fugou(i, 1));
                                item.CanMovePosList.Add(new Fugou(i, 0));
                            }
                            item.CanMovePosList.Add(new Fugou(0, -1));
                        }
                        else if (item.Name == "角行" && item.Onclick == 1)
                        {
                            item.Image = Image.FromFile(@"Pieces\uma.png");
                            item.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                            item.Name = "龍馬";
                            item.CanMovePosList.Clear();
                            for (int i = -8; i <= 8; i++)
                            {
                                item.CanMovePosList.Add(new Fugou(i, i));
                                item.CanMovePosList.Add(new Fugou(i, -i));
                            }
                            item.CanMovePosList.Add(new Fugou(0, -1));
                            item.CanMovePosList.Add(new Fugou(0, 1));
                            item.CanMovePosList.Add(new Fugou(-1, 0));
                            item.CanMovePosList.Add(new Fugou(1, 0));
                        }
                        else if (item.Name == "飛車" && item.Onclick == 1)
                        {
                            item.Image = Image.FromFile(@"Pieces\ryu.png");
                            item.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                            item.Name = "龍王";
                            item.CanMovePosList.Clear();
                            for (int i = -8; i <= 8; i++)
                            {
                                item.CanMovePosList.Add(new Fugou(0, i));
                                item.CanMovePosList.Add(new Fugou(i, 0));
                            }
                            item.CanMovePosList.Add(new Fugou(-1, -1));
                            item.CanMovePosList.Add(new Fugou(1, 1));
                            item.CanMovePosList.Add(new Fugou(-1, 1));
                            item.CanMovePosList.Add(new Fugou(1, -1));
                        }
                    }
                }
                reverseSelect = MessageBoxResult.No;
            }
            else if (reverseSelect == MessageBoxResult.No)
            {
                // 「いいえ」ボタンを押した場合の処理
                return;
            }
        }


        //成るか確認フォーム表示
        public void ShowReveaseWindow()
        {
            if (CanReverse(form1.playerPieces, form1.comPieces))
            {
                reverseSelect = System.Windows.MessageBox.Show("成りますか？", "", MessageBoxButton.YesNo);
            }
        }

        //成れるか判断
        public bool CanReverse(List<Piece> playerPieces, List<Piece> comPieces)
        {
            var temp = new Piece(form1);

            if (turnManager.turn == Turn.PLAYERTURN && playerPieces.Any(n => n.Onclick == 1))
            {
                temp = playerPieces.FirstOrDefault(n => n.Onclick == 1);
                if (temp.Name == "歩兵" || temp.Name == "香車" || temp.Name == "桂馬" || temp.Name == "銀将" ||
                   temp.Name == "金将" || temp.Name == "王将" || temp.Name == "角行" || temp.Name == "飛車")
                {
                    if (this.Fugou.Y <= 3) return true;
                    else if (temp.Fugou.Y <= 3) return true;
                    else return false;
                }
                else return false;
            }
            else if (turnManager.turn == Turn.COMTURN && comPieces.Any(n => n.Onclick == 1))
            {
                temp = comPieces.FirstOrDefault(n => n.Onclick == 1);
                if (temp.Name == "歩兵" || temp.Name == "香車" || temp.Name == "桂馬" || temp.Name == "銀将" ||
                  temp.Name == "金将" || temp.Name == "王将" || temp.Name == "角行" || temp.Name == "飛車")
                {
                    if (this.Fugou.Y >= 7) return true;
                    else if (temp.Fugou.Y >= 7) return true;
                    else return false;
                }
                else return false;
            }
            else return false;
        }
    }
}
