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
badd +46 src/client/client.cs
badd +24 ~/source/MagicOnion/samples/ChatApp/ChatApp.Console/Program.cs
badd +1 ~/.config/nvim/init.lua
badd +1 ~/source/networked-state-machine
badd +1 ~/source/MagicOnion/samples/ChatApp/ChatApp.Shared/Hubs/IChatHub.cs
badd +1 ~/source/MagicOnion/samples/ChatApp/ChatApp.Shared/Hubs/IChatHubReceiver.cs
badd +15 ~/source/MagicOnion/samples/ChatApp/ChatApp.Server/ChatHub.cs
badd +1 todo
badd +1 test/state_machine_test.cs
badd +1 term://~/source/MagicOnion//260563:/bin/bash
badd +1 ~/source/Godot-Advanced-State-Machine-First-Person-Controller
argglobal
%argdel
$argadd ~/source/networked-state-machine
set stal=2
tabnew +setlocal\ bufhidden=wipe
tabnew +setlocal\ bufhidden=wipe
tabrewind
edit test/state_machine_test.cs
let s:save_splitbelow = &splitbelow
let s:save_splitright = &splitright
set splitbelow splitright
wincmd _ | wincmd |
vsplit
1wincmd h
wincmd w
let &splitbelow = s:save_splitbelow
let &splitright = s:save_splitright
wincmd t
let s:save_winminheight = &winminheight
let s:save_winminwidth = &winminwidth
set winminheight=0
set winheight=1
set winminwidth=0
set winwidth=1
exe 'vert 1resize ' . ((&columns * 143 + 143) / 286)
exe 'vert 2resize ' . ((&columns * 142 + 143) / 286)
argglobal
balt todo
setlocal foldmethod=manual
setlocal foldexpr=0
setlocal foldmarker={{{,}}}
setlocal foldignore=#
setlocal foldlevel=0
setlocal foldminlines=1
setlocal foldnestmax=20
setlocal foldenable
silent! normal! zE
let &fdl = &fdl
let s:l = 108 - ((33 * winheight(0) + 24) / 49)
if s:l < 1 | let s:l = 1 | endif
keepjumps exe s:l
normal! zt
keepjumps 108
normal! 05|
lcd ~/source/networked-state-machine
wincmd w
argglobal
if bufexists(fnamemodify("~/source/networked-state-machine/todo", ":p")) | buffer ~/source/networked-state-machine/todo | else | edit ~/source/networked-state-machine/todo | endif
if &buftype ==# 'terminal'
  silent file ~/source/networked-state-machine/todo
endif
balt ~/source/networked-state-machine/test/state_machine_test.cs
setlocal foldmethod=manual
setlocal foldexpr=0
setlocal foldmarker={{{,}}}
setlocal foldignore=#
setlocal foldlevel=0
setlocal foldminlines=1
setlocal foldnestmax=20
setlocal foldenable
silent! normal! zE
let &fdl = &fdl
let s:l = 28 - ((27 * winheight(0) + 24) / 49)
if s:l < 1 | let s:l = 1 | endif
keepjumps exe s:l
normal! zt
keepjumps 28
normal! 0
lcd ~/source/networked-state-machine
wincmd w
exe 'vert 1resize ' . ((&columns * 143 + 143) / 286)
exe 'vert 2resize ' . ((&columns * 142 + 143) / 286)
tabnext
edit ~/source/MagicOnion/samples/ChatApp/ChatApp.Shared/Hubs/IChatHub.cs
let s:save_splitbelow = &splitbelow
let s:save_splitright = &splitright
set splitbelow splitright
wincmd _ | wincmd |
vsplit
1wincmd h
wincmd w
let &splitbelow = s:save_splitbelow
let &splitright = s:save_splitright
wincmd t
set winminheight=0
set winheight=1
set winminwidth=0
set winwidth=1
exe 'vert 1resize ' . ((&columns * 168 + 143) / 286)
exe 'vert 2resize ' . ((&columns * 117 + 143) / 286)
tcd ~/source/MagicOnion
argglobal
balt ~/source/MagicOnion/samples/ChatApp/ChatApp.Server/ChatHub.cs
setlocal foldmethod=manual
setlocal foldexpr=0
setlocal foldmarker={{{,}}}
setlocal foldignore=#
setlocal foldlevel=0
setlocal foldminlines=1
setlocal foldnestmax=20
setlocal foldenable
silent! normal! zE
let &fdl = &fdl
let s:l = 12 - ((0 * winheight(0) + 24) / 49)
if s:l < 1 | let s:l = 1 | endif
keepjumps exe s:l
normal! zt
keepjumps 12
normal! 022|
wincmd w
argglobal
if bufexists(fnamemodify("term://~/source/MagicOnion//260563:/bin/bash", ":p")) | buffer term://~/source/MagicOnion//260563:/bin/bash | else | edit term://~/source/MagicOnion//260563:/bin/bash | endif
if &buftype ==# 'terminal'
  silent file term://~/source/MagicOnion//260563:/bin/bash
endif
balt ~/source/MagicOnion/samples/ChatApp/ChatApp.Console/Program.cs
setlocal foldmethod=manual
setlocal foldexpr=0
setlocal foldmarker={{{,}}}
setlocal foldignore=#
setlocal foldlevel=0
setlocal foldminlines=1
setlocal foldnestmax=20
setlocal foldenable
let s:l = 44 - ((43 * winheight(0) + 24) / 49)
if s:l < 1 | let s:l = 1 | endif
keepjumps exe s:l
normal! zt
keepjumps 44
normal! 0
wincmd w
exe 'vert 1resize ' . ((&columns * 168 + 143) / 286)
exe 'vert 2resize ' . ((&columns * 117 + 143) / 286)
tabnext
edit ~/source/Godot-Advanced-State-Machine-First-Person-Controller/addons/JehenoAdvancedFirstPersonController/PlayerCharacter/StateMachine
wincmd t
set winminheight=0
set winheight=1
set winminwidth=0
set winwidth=1
tcd ~/source/Godot-Advanced-State-Machine-First-Person-Controller/addons/JehenoAdvancedFirstPersonController/PlayerCharacter/StateMachine
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
let &fdl = &fdl
let s:l = 8 - ((7 * winheight(0) + 24) / 49)
if s:l < 1 | let s:l = 1 | endif
keepjumps exe s:l
normal! zt
keepjumps 8
normal! 0
lcd ~/source/Godot-Advanced-State-Machine-First-Person-Controller/addons/JehenoAdvancedFirstPersonController/PlayerCharacter/StateMachine
tabnext 3
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
