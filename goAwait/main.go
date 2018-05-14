package main

import (
	"fmt"
	"time"
)

func doWork(name string, x int, sec int, c chan int) {
	fmt.Println(fmt.Sprintf("Start: %v", name))
	time.Sleep(time.Duration(sec) * time.Second)
	fmt.Println(fmt.Sprintf("End: %v", name))
	c <- x
}

func flow5_6(c chan int) {
	var subTotal int
	cf := make(chan int)
	go doWork("5", 55, 4, cf)
	subTotal += <-cf
	go doWork("6", 60, 2, cf)
	subTotal += <-cf
	c <- subTotal
}

func elapsed() func() {
	start := time.Now()
	return func() {
		fmt.Printf("Execution time: %v", int(time.Since(start).Seconds()+0.5))
	}
}

func main() {
	defer elapsed()()
	var total int
	c := make(chan int)
	go doWork("1", 10, 2, c)
	total += <-c
	go doWork("2", 25, 1, c)
	total += <-c

	c3 := make(chan int)
	c4 := make(chan int)
	go doWork("3", 30, 7, c3)
	go doWork("4", 45, 2, c4)
	go flow5_6(c)
	total += <-c3 + <-c4 + <-c

	fmt.Println(fmt.Sprintf("Total: %v", total))
}
