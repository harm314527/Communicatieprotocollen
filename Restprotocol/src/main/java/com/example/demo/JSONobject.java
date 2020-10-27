package com.example.demo;
import com.fasterxml.jackson.core.JsonGenerationException;
import org.json.simple.JSONObject;


public class JSONobject {

    private JSONObject myjson = null;
    public JSONobject(int Low, int High) {
        int random = ((int) (Math.random() * (High - Low))) + Low;
        myjson = new JSONObject();
        myjson.put("Lowerbound", Low);
        myjson.put("Higherbound", High);
        myjson.put("RandomNumber", random);
    }
    public JSONObject returnJson()
    {
            return myjson;
    }
}
