#!/bin/bash    
ml icc                                                                                 
for file in `find ./ -type  f -name '*\.out'`                                                                                                                                        
do        
	echo "*******************************"																																		
	echo "$file for 10 times executed in"                                                                                                                                               
	time `for i in {1..10}; do ./file; done`
done                                                                                                                                                 
