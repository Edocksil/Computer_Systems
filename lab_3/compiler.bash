#!/bin/bash                                                                                                                                                           
flags=`grep ^flags /proc/cpuinfo | uniq | cut -d':' -f2 | cut -d" " -f2- | tr _ . | tr "a-z" "A-Z"`
ml icc                                                                     
        for flag in $flags; do
                                                                                                                                                                        
                icc -O1 -x$flag fibo.cpp -o test.out 2> error
                                                                                                                                                                        
                if [ ! -s "error" ]; then #check if error occured - so the flag is compatible   
                                                                                                                                                                        
                        for o in {1..3}; do
                                echo "---------------------------" 
                                echo "$flag with -O$o"             
                                time `icc -O$o -x$flag fibo.cpp -o $flag_o$o.out` 
                        done
                fi
        done