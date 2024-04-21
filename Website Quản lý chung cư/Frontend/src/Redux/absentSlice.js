import { createSlice, createAsyncThunk, createAction } from "@reduxjs/toolkit";
import absentService from "../Services/API/absentService";
export const fetchAllAbsents = createAsyncThunk(
    "absent/fetchAllAbsents",
    async (_, thunkAPI) => {
        try {
            return await absentService.getListAbsent();
        } catch (e) {
            console.log("error", e);
            return thunkAPI.rejectWithValue("something went wrong");
        }
    }
);
export const resetAbsentSlice = createAction("resetAbsentSlice");
const initialState = {
    absentList: [],
    isLoading: false,
};
const absentSlice = createSlice({
    name: "absent",
    initialState,
    reducers: {},
    extraReducers: {
        [fetchAllAbsents.pending]: (state) => {
            console.log("pending");
            state.isLoading = true;
        },
        [fetchAllAbsents.fulfilled]: (state, action) => {
            console.log("actiion ful", action);
            state.isLoading = false;
            state.absentList = action?.payload?.data;
        },
        [fetchAllAbsents.rejected]: (state, action) => {
            console.log("action rej", action);
            state.isLoading = false;
        },
        [resetAbsentSlice]: () => initialState
    }
});
export default absentSlice.reducer;