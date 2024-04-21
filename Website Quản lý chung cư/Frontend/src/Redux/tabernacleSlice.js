import { createSlice, createAsyncThunk, createAction } from "@reduxjs/toolkit";
import tabernacleService from "../Services/API/tabernacleService";
export const fetchAllTabernacles = createAsyncThunk(
    "tabernacle/fetchAllTabernacles",
    async (_, thunkAPI) => {
        try {
            return await tabernacleService.getListTabernacle();
        } catch (e) {
            console.log("error", e);
            return thunkAPI.rejectWithValue("something went wrong");
        }
    }
);
export const resetTabernacleSlice = createAction("resetTabernacleSlice");
const initialState = {
    tabernacleList: [],
    isLoading: false,
};
const tabernacleSlice = createSlice({
    name: "tabernacle",
    initialState,
    reducers: {},
    extraReducers: {
        [fetchAllTabernacles.pending]: (state) => {
            console.log("pending");
            state.isLoading = true;
        },
        [fetchAllTabernacles.fulfilled]: (state, action) => {
            console.log("actiion ful", action);
            state.isLoading = false;
            state.tabernacleList = action?.payload?.data;
        },
        [fetchAllTabernacles.rejected]: (state, action) => {
            console.log("action rej", action);
            state.isLoading = false;
        },
        [resetTabernacleSlice]: () => initialState
    }
});
export default tabernacleSlice.reducer;