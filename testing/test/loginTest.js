
const {Builder, By, Key, Browser, until} = require('selenium-webdriver');
var should = require('chai').should();


async function autoLogin(driver){


        await driver.get("http://localhost:5173/login")

        let UserNameInput = await driver.findElement(By.xpath("/html/body/div/div/div[2]/input[1]")).sendKeys("Admin")
        let PassworInput = await driver.findElement(By.xpath("/html/body/div/div/div[2]/input[2]")).sendKeys("12345") 

        let logInButton = await driver.findElement(By.xpath("/html/body/div/div/div[2]/div/button[1]"))
        logInButton.click();

        await driver.manage().setTimeouts({implicit: 500});

        let currentURL = await driver.getCurrentUrl()
        currentURL.should.equal("http://localhost:5173/")
    }

describe("Log In Tests", function(){
    it("Should stay in the same page if log-in blank",async function(){
        let driver = await new Builder().forBrowser('firefox').build();

        await driver.get("http://localhost:5173/login")

        let logInButton = await driver.findElement(By.xpath("/html/body/div/div/div[2]/div/button[1]"))
        logInButton.click();
        
        let currentURL = await driver.getCurrentUrl()
        currentURL.should.equal("http://localhost:5173/login")
        await driver.quit();
    })

    it("Should move to next page if sign in is correct",async () =>{
        let driver = await new Builder().forBrowser('firefox').build();
        autoLogin(driver)
        driver.quit();
    } )
})

describe("View Items Tests", () =>{
    it("Should move to redirect to login when not logged in",async ()=>{
        let driver = await new Builder().forBrowser('firefox').build();
        await driver.get("http://localhost:5173/");

        await driver.manage().setTimeouts({implicit: 500});

        let currentURL = await driver.getCurrentUrl()
        currentURL.should.equal("http://localhost:5173/login")

    })
})

describe("Add Item Test", () => {
    it("Should through an error if field is empty",async ()=>{
        let driver = await new Builder().forBrowser('firefox').build();
        await autoLogin(driver);

        await driver.findElement(By.xpath("/html/body/div/div/div[2]/button")).click();
        await driver.manage().setTimeouts({implicit: 1000});
        let message = await driver.findElement(By.css("html body div#app div div div.main-card div div button")).getText();

        message.should.equal(message,"All text fields must be full")
        driver.quit();
    })

})

describe("Edit Employee Test", async () => {
    it("Should be successful if all only one field is changed", async () => {
        let driver = await new Builder().forBrowser('firefox').build();
        await autoLogin(driver);
        await driver.manage().setTimeouts({implicit: 500});

        await driver.findElement(By.css("html body div#app div div table tr td button.btn")).click();
        await driver.manage().setTimeouts({implicit: 1000});

        let message = await driver.findElement(By.css("html body div#app div div div.notif-good.notif p b")).getText();

        message.should.equal(message,"The employee 'César Aybar' was successfully updated?")
        driver.quit();
    })
})

