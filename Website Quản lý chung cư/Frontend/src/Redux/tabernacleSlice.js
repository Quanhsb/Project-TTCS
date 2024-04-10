import { createSlice, createAsyncThunk, createAction } from "@reduxjs/toolkit";
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