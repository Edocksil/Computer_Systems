#!/bin/bash    
                                                                                                             
for file in `ls ./`                                                                                                                                        
do                                                                                                                                                   
	echo $file                                                                                                                                              
	time `for i in {1..10}; do
		./file
	done`
done                                                                                                                                                 
