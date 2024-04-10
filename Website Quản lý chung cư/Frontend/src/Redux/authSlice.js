import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
export const logout = createAsyncThunk("auth/logout", async () => {
  await authService.logout();
  localStorage.removeItem("accessToken");
  localStorage.removeItem("userName");
});
const storeToke = (token) => {
  console.log(token);
  localStorage.setItem("accessToken", token);
};
const storeUserName = (userName) => {
  localStorage.setItem("userName", userName);
};
const getAccessToken = () => {
  const accessToken = localStorage.getItem("accessToken");
  console.log(`Bearer ${localStorage.getItem("accessToken")}`);
  if (accessToken !== "undefined" && accessToken) {
    return accessToken;
  }
  return "";
};
const initialState = {
  user: null,
  isAuthenticated: false,
  isLoading: true,
  loginType: null
};
const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {},
  extraReducers: {
    [login.pending]: (state) => {
      console.log("pending");
      state.isLoading = true;
    },
    [login.fulfilled]: (state, action) => {
      console.log("actiion ful", action.payload.data);
      state.isLoading = false;
      state.user = action?.payload?.data;
      state.isAuthenticated = true;
      state.loginType = true;
      storeToke(action?.payload?.data?.token);
      storeUserName(action?.payload?.data?.userName);
    },
    [login.rejected]: (state, action) => {
      console.log("action reject", action);
      state.isLoading = false;
      state.isAuthenticated = false;
      state.loginType = false;
    },
    [loadUser.pending]: (state) => {
      console.log("pending");
      state.isLoading = true;
    },
    [loadUser.fulfilled]: (state, action) => {
      console.log("actiion ful", action);
      state.isLoading = false;
      state.user = action?.payload?.data;
      console.log("state us", state.user);
      state.isAuthenticated = true;
    },
    [loadUser.rejected]: (state, action) => {
      console.log("action reject", action);
      state.isLoading = false;
      state.isAuthenticated = false;
      state.user = null;
      logout();
    },
    [logout.fulfilled]: (state) => {
      state.isAuthenticated = false;
      state.user = null;
    },
  },
});
export const _ = authSlice.actions;
export default authSlice.reducer;
