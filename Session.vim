let SessionLoad = 1
let s:so_save = &g:so | let s:siso_save = &g:siso | setg so=0 siso=0 | setl so=-1 siso=-1
let v:this_session=expand("<sfile>:p")
doautoall SessionLoadPre
silent only
silent tabonly

cd ~/source/networked-state-machine

if expand('%') == '' && !&modified && line('$') <= 1 && getline(1) == ''
  let s:wipebuf = bufnr('%')
endif

let s:shortmess_save = &shortmess
set shortmess+=aoO

badd +2 src/client/client.cs
badd +11 ~/source/MagicOnion/samples/ChatApp/ChatApp.Console/Program.cs
badd +1 ~/.config/nvim/init.lua

argglobal
%argdel
$argadd ~/source/networked-state-machine/

set stal=2

" TAB 1
edit ~/source/networked-state-machine
lcd ~/source/networked-state-machine

let s:save_winminheight = &winminheight
let s:save_winminwidth = &winminwidth

set winminheight=0
set winheight=1
set winminwidth=0
set winwidth=1

argglobal

setlocal foldmethod=manual
setlocal foldexpr=0
setlocal foldmarker={{{,}}}
setlocal foldignore=#
setlocal foldlevel=0
setlocal foldminlines=1
setlocal foldnestmax=20
setlocal foldenable

silent! normal! zE

let s:l = 8 - ((7 * winheight(0) + 20) / 41)
if s:l < 1 | let s:l = 1 | endif
keepjumps exe s:l
normal! zt
keepjumps 8
normal! 0


" TAB 2
tabnew
edit ~/source/MagicOnion/samples/ChatApp/ChatApp.Console/Program.cs

lcd!
tcd ~/source/MagicOnion

argglobal

setlocal foldmethod=manual
setlocal foldexpr=0
setlocal foldmarker={{{,}}}
setlocal foldignore=#
setlocal foldlevel=0
setlocal foldminlines=1
setlocal foldnestmax=20
setlocal foldenable

silent! normal! zE

let s:l = 11 - ((10 * winheight(0) + 20) / 41)
if s:l < 1 | let s:l = 1 | endif
keepjumps exe s:l
normal! zt
keepjumps 11
normal! 0

" Ensure this tab has no window-local cwd overriding tcd.
lcd!
tcd ~/source/MagicOnion


tabnext 1
set stal=1

if exists('s:wipebuf') && len(win_findbuf(s:wipebuf)) == 0 && getbufvar(s:wipebuf, '&buftype') isnot# 'terminal'
  silent exe 'bwipe ' . s:wipebuf
endif

unlet! s:wipebuf

set winheight=1 winwidth=20

let &shortmess = s:shortmess_save
let &winminheight = s:save_winminheight
let &winminwidth = s:save_winminwidth

let s:sx = expand("<sfile>:p:r")."x.vim"
if filereadable(s:sx)
  exe "source " . fnameescape(s:sx)
endif

let &g:so = s:so_save | let &g:siso = s:siso_save

set hlsearch
nohlsearch

doautoall SessionLoadPost
unlet SessionLoad

" vim: set ft=vim :
