import { configureStore } from "@reduxjs/toolkit";
import userReducer from "./userSlice";
import authReducer from "./authSlice";
import privilegeReducer from "./privilegeSlice";
import tabernacleReducer from "./tabernacleSlice";
import absentReducer from "./absentSlice";
import revenueReducer from "./revenueSlice";
import detailroomSlice from "./detailRoomSlice";
import demographicReducer from "./demographicSlice";
export const store = configureStore({
  reducer: {
    user: userReducer,
    auth: authReducer,
    privilege: privilegeReducer,
    tabernacle: tabernacleReducer,
    absent: absentReducer,
    revenue: revenueReducer,
    demographic: demographicReducer,
    isDetailVisible: detailroomSlice.reducer
  },
});
