import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
const initialState = {
  userList: [],
  isLoading: false,
};
const userSlice = createSlice({
  name: "user",
  initialState,
  reducers: {},
  extraReducers: {
    [fetchAllUsers.pending]: (state) => {
      console.log("pending");
      state.isLoading = true;
    },
    [fetchAllUsers.fulfilled]: (state, action) => {
      console.log("actiion ful", action);
      state.isLoading = false;
      state.userList = action?.payload?.data;
    },
    [fetchAllUsers.rejected]: (state, action) => {
      console.log("action rej", action);
      state.isLoading = false;
    },
  },
});
export const _ = userSlice.actions;
export default userSlice.reducer;
