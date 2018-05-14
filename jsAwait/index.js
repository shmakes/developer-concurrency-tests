function doWork(name,x,sec) {
  return new Promise(resolve => {
  console.log('Start: ' + name);
    setTimeout(() => {
        console.log('End: ' + name);
      resolve(x);
    }, sec *1000);
  });
}

async function complexFlow() {
  try {
    let start = new Date();
 
    let result1 = await doWork(1, 10, 2);
    let result2 = await doWork(2, 25, 1);
    let finalResult = result1 + result2;

    let Flow5_6 = async function() {
      let result5 = await doWork(5, 55, 4);
      let result6 = await doWork(6, 60, 2);
      return result5 + result6;
    }
 
    let promises = [doWork(3, 30, 7), doWork(4, 45, 2), Flow5_6()];
    let results = promises.map(async (job) => await job);
    for (const result of results) {
      finalResult += (await result);
    }
    console.log("Total: " + finalResult);
    var end = new Date() - start;
    console.info("Execution time: %d seconds", Math.round(end/1000));
    return finalResult;
 
  } catch (err) {
    console.log(err);
  }
}
 
complexFlow();
