import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
const initialState = {
  privilegeList: [],
  roleList: [],
};
const privilegeSlice = createSlice({
  name: "privilege",
  initialState,
  reducers: {},
  extraReducers: {
    [fetchRoleList.pending]: (state) => {
      console.log("pending");
    },
    [fetchRoleList.fulfilled]: (state, action) => {
      console.log("actiion fullfil", action);
      state.roleList = action?.payload?.data;
    },
    [fetchRoleList.rejected]: (state, action) => {
      console.log("action rej", action);
    },
  },
});
export const _ = privilegeSlice.actions;
export default privilegeSlice.reducer;
