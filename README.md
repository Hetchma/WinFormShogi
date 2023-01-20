# 将棋（Win フォームアプリ）

## 更新履歴

- 2021/02/03(初回)

## 使い方

- ZIP ダウンロード(画面上部の緑色 Code ボタン →Download ZIP)
- WinFormShogi.sin ファイルを VisualStudio で起動
- デバッグ無しで開始
- 一度エラーが出ますが、一旦終了
- VIsualStudio も終了
- フォルダ「Pieces」を、WinFormShogi → bin → Debug フォルダ内に移動
- Debug フォルダの WinFormShogi アプリケーションを起動してスタート

## 概要

C#を学習して初めて製作したアプリです

- 将棋ゲームアプリ
- COM 側自動着手は未実装

## コード説明

### Form1

- 盤の描画
- 9\*9=81 マスに空の Piece を配置
- 対局持ち時間選択後、対局開始ボタンで駒を初期配置（指定 Piece に駒情報を渡す）
- 手番・手数・持ち時間表示
- 持ち時間管理

### TurnManager

- 先手・後手の決定
- 先手・後手のターン管理
- 勝敗決定（王将奪取 or 時間切れ）

### Piece

- 駒クラス（PictureBox を継承）
- 駒の操作・ルールは全てこのクラスに記載

### Turn

- enum 先手・後手

### Owner

- enum 自分・相手・空

### Fugou

- Struct 符号（将棋ルールと同等右上が(1,1)）
