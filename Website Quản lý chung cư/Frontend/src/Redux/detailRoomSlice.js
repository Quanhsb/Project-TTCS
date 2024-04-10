import { createSlice } from '@reduxjs/toolkit';
const detailroomSlice = createSlice({
  name: 'detailroom',
  initialState: {
    isDetailVisible: false,
    isSelectedId: "",
  },
  reducers: {
    isDetailVisibleChange: (state) => {
      state.isDetailVisible = !state.isDetailVisible;
      console.log(state.isDetailVisible);
    },
    isSelectedIdChange: (state, action) => {
      state.isSelectedId = action.payload;
    }
  },
});
export default detailroomSlice;

