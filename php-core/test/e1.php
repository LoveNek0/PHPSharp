<?php
$var = 0;
while(1 + 2){
	echo 1;
}
while(1 + 2)
	echo 2;
for($i = 0; $i; ++$i){
	echo 3;
}
for($i = 0; $i; $i++)
	echo 3 + 3 + 4;

$var = (()$a + 231);

$b = function (int ...$a){
	echo 123 + $a;
	function f() use ($a){
		$c = 123 + $a;
	}
}
