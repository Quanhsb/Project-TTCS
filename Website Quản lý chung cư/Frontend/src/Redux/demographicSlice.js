import { createSlice, createAsyncThunk, createAction } from "@reduxjs/toolkit";
import demographicService from "../Services/API/demographicService";
export const fetchAllDemographic = createAsyncThunk(
    "demographic/fetchAllDemographic",
    async (_, thunkAPI) => {
        try {
            return await demographicService.getListDemographic();
        } catch (e) {
            console.log("error", e);
            return thunkAPI.rejectWithValue("something went wrong");
        }
    }
);
export const resetDemographicSlice = createAction("resetDemographicSlice");
const initialState = {
    demographicList: [],
    isLoading: false,
};
const demographicSlice = createSlice({
    name: "demographic",
    initialState,
    reducers: {},
    extraReducers: {
        [fetchAllDemographic.pending]: (state) => {
            console.log("pending");
            state.isLoading = true;
        },
        [fetchAllDemographic.fulfilled]: (state, action) => {
            console.log("actiion ful", action);
            state.isLoading = false;
            state.demographicList = action?.payload?.data;
        },
        [fetchAllDemographic.rejected]: (state, action) => {
            console.log("action rej", action);
            state.isLoading = false;
        },
        [resetDemographicSlice]: () => initialState
    }
});
export default demographicSlice.reducer;