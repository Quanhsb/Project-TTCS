import { createSlice, createAsyncThunk, createAction } from "@reduxjs/toolkit";
import revenueService from "../Services/API/revenueService";
export const fetchAllRevenue = createAsyncThunk(
    "revenue/fetchAllRevenue",
    async (_, thunkAPI) => {
        try {
            return await revenueService.getListRevenue();
        } catch (e) {
            console.log("error", e);
            return thunkAPI.rejectWithValue("something went wrong");
        }
    }
);
export const fetchRevenueItem = createAsyncThunk(
    "revenue/fetchRevenueItem",
    async (maKhoanThu, thunkAPI) => {
        try {
            return await revenueService.getRevenue(maKhoanThu);
        } catch (e) {
            console.log("error", e);
            return thunkAPI.rejectWithValue("something went wrong");
        }
    }
)
export const fetchAllRevenueHouse = createAsyncThunk(
    "revenue/fetchAllRevenueHouse",
    async (maHoKhau, thunkAPI) => {
        try {
            return await revenueService.getRevenueHouse(maHoKhau);
        } catch (e) {
            console.log("error", e);
            return thunkAPI.rejectWithValue("something went wrong");
        }
    }
)
export const setRevenueItemID = createAction("setRevenueItemID");
export const setRevenueItemType = createAction("setRevenueItemType");
export const setRevenueHouseID = createAction("setRevenueHouseID");
export const resetRevenueSlice = createAction("resetRevenueSlice");
const initialState = {
    revenueList: [],
    isLoadingList: false,
    revenueItem: [],
    isLoadingItem: [],
    maKhoanThu: null,
    loaiKhoanThu: null,
    revenueHouse: [],
    isLoadingHouse: false,
    revenueHouseID: null,
};
const revenueSlice = createSlice({
    name: "revenue",
    initialState,
    reducers: {},
    extraReducers: {
        [fetchAllRevenue.pending]: (state) => {
            console.log("pending");
            state.isLoadingList = true;
        },
        [fetchAllRevenue.fulfilled]: (state, action) => {
            console.log("actiion ful", action);
            state.isLoadingList = false;
            state.revenueList = action?.payload?.data;
        },
        [fetchAllRevenue.rejected]: (state, action) => {
            console.log("action rej", action);
            state.isLoadingList = false;
        },
        [fetchAllRevenueHouse.pending]: (state) => {
            console.log("pending");
            state.isLoadingHouse = true;
        },
        [fetchAllRevenueHouse.fulfilled]: (state, action) => {
            console.log("actiion ful", action);
            state.isLoadingHouse = false;
            state.revenueHouse = action?.payload?.data;
        },
        [fetchAllRevenueHouse.rejected]: (state, action) => {
            console.log("action rej", action);
            state.isLoadingHouse = false;
        },
        [fetchRevenueItem.pending]: (state) => {
            console.log("pending");
            state.isLoadingItem = true;
        },
        [fetchRevenueItem.fulfilled]: (state, action) => {
            console.log("actiion ful", action);
            state.isLoadingItem = false;
            state.revenueItem = action?.payload?.data?.data;
        },
        [fetchRevenueItem.rejected]: (state, action) => {
            console.log("action rej", action);
            state.isLoadingItem = false;
        },
        [setRevenueItemID]: (state, action) => {
            state.maKhoanThu = action?.payload;
        },
        [setRevenueItemType]: (state, action) => {
            state.loaiKhoanThu = action?.payload;
        },
        [setRevenueHouseID]: (state, action) => {
            state.revenueHouseID = action?.payload;
        },
        [resetRevenueSlice]: () => initialState
    }
});
export default revenueSlice.reducer;