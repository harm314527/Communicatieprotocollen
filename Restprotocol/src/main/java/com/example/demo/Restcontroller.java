package com.example.demo;

import org.json.simple.JSONObject;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseBody;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class Restcontroller {
    @GetMapping("Check")
    public String Test()
    {
        return "Confirmed";
    }

    @GetMapping("/RandomValueInBound")
    @ResponseBody
    public JSONObject Randomvalue(@RequestParam int Low, @RequestParam int High)
    {
        JSONobject myjson = new JSONobject(Low,High);
        return myjson.returnJson();

    }
}
