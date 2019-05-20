;macro for printing text. example: 'print info, info_length'
	%macro print 2
mov rax, SYS_WRITE
mov rdi, STDOUT
mov rsi, %1 	;first argument (info)
mov rdx, %2	;second argument {info_length)
syscall
	%endmacro

	section .data
;defined Linux System Calls for x64
%define SYS_WRITE 1
%define STDOUT 1
%define SYS_EXIT 60

newline db 10, 0	;newline implementation in NASM
nl_len: equ $-newline 	;length of new line

msg db "This is env command via assembler: ", 10, 0 	;message with new line
msg_len: equ $-msg 	;calculated length of message

	section .bss
envp: resq 1	;variable for strings of env command

	section .text
global _start
_start:
print msg, msg_len

mov rbx, qword [rsp]	; argc = *(%rsp)
lea rcx, [rsp + rbx*8 + 16] ;needed offset of cmd args ;rcx = %rsp +8 * (argc + 2)
mov qword [envp], rcx	; **envp = rcx

loop:	;while (envp != NULL)
mov rcx, [envp]		;temp var for transfering value
mov rsi, qword [rcx]	;p = *envp
mov rdi, rsi		;temp for output
xor rdx, rdx		;len = 0

;loop to count length of each row of environmnet array
string_count:		;while (*p != '\0')
cmp byte [rdi], 0	;check of LSB
je output
inc rdi			;p++
inc rdx			;len++
jmp string_count

output: ;printing string (%rdi is p, %rdx is length)
mov rax, SYS_WRITE
mov rdi, STDOUT
syscall

add qword [envp], 8	;offset to next element of env
mov r8, [envp]		;temp var for check
cmp qword [r8], 0
je end
cmp qword [envp], 0
je end

print newline, nl_len
jmp loop

end: ;exit from program
print newline, nl_len
mov rax, SYS_EXIT 
xor rdi, rdi
syscall
