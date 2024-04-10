import { Box, Button, TextField, Typography, Dialog, DialogTitle, DialogContent, IconButton, MenuItem } from "@mui/material";
import { Formik } from "formik";
import * as yup from "yup";
import useMediaQuery from "@mui/material/useMediaQuery";
import SaveAsIcon from '@mui/icons-material/SaveAs';
import { useEffect, useState, } from "react";
import { useDispatch } from "react-redux";
import { fetchAllAbsents } from "../../Redux/absentSlice";
import CloseIcon from '@mui/icons-material/Close';
import absentService from "../../Services/API/absentService";
import { DesktopDatePicker, LocalizationProvider, } from "@mui/x-date-pickers";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import dayjs from "dayjs";
import axios from "../../setups/custom_axios";
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
const RegisterAbsent = ({ openPopup, setOpenPopup }) => {
  const isNonMobile = useMediaQuery("(min-width:600px)");
  const [dataNk, setDataNk] = useState([]);
  const [date, setDate] = useState(dayjs(new Date()));
  const [maNhanKhau, setMaNhanKhau] = useState();
  const dispatch = useDispatch();
  const handleFormSubmit = (values) => {
    if (window.confirm("Bạn chắc chắn muốn lưu?") === true) {
      absentService.postAbsent({
        maNhanKhau: maNhanKhau,
        thoiHan: date,
        lyDo: values.lyDo,
      }).then(mes => {
        toast(mes.message);
        setOpenPopup(!openPopup);
        dispatch(fetchAllAbsents());
      }).catch(e => {
        toast(e?.response?.data?.reason || "Thêm mới tạm vắng thất bại")
      })
    }
  };
  const handleGetDataNhanKhauChuaDkiTamVang = () => {
    axios.get(`/nhan-khau/danh-sach-nhan-khau-chua-dang-ky-tam-vang`).then((result) => {
      setDataNk(result.data);
    }).catch(e => {
      setDataNk([]);
    })
  }
  const handleOnChange = (newValue) => {
    setDate(newValue);
  }
  useEffect(() => {
    if (openPopup) {
      handleGetDataNhanKhauChuaDkiTamVang()
    }
  }, [openPopup]);
  const initialValues = {
    nhanKhau: "",
    lyDo: "",
  };
  return (
    <Dialog open={openPopup} maxWidth="md" style={{ backgroundColor: "transparent" }}
      sx={{
      }}>
      <DialogTitle>
        <div style={{ display: 'flex' }}>
          <Typography variant="h6" component="div" style={{ flexGrow: 1, fontSize: 20, fontWeight: "bold" }}>
            {"Đăng ký tạm vắng"}
          </Typography>
          <IconButton aria-label="close" onClick={() => {
            setMaNhanKhau(undefined);
            setOpenPopup(!openPopup);
          }}>
            <CloseIcon></CloseIcon>
          </IconButton>
        </div>
      </DialogTitle>
      <DialogContent dividers>
        <Box m="20px">
          <Formik
            onSubmit={handleFormSubmit}
            initialValues={initialValues}
            validationSchema={checkoutSchema}
          >
            {({
              values,
              errors,
              touched,
              handleBlur,
              handleChange,
              handleSubmit,
            }) => (
              <form onSubmit={handleSubmit}>
                <Box
                  display="grid"
                  gap="30px"
                  gridTemplateColumns="repeat(4, minmax(0, 1fr))"
                  sx={{
                    "& > div": { gridColumn: isNonMobile ? undefined : "span 4" },
                    width: 500
                  }}
                >
                  <TextField
                    variant="filled"
                    select
                    label="Nhân khẩu"
                    onBlur={handleBlur}
                    name="nhanKhau"
                    onChange={(e) => setMaNhanKhau(+e.target.value)}
                    error={!!touched.nhanKhau && !maNhanKhau}
                    helperText={!!touched.nhanKhau && !maNhanKhau && "Bạn chưa chọn nhân khẩu"}
                    value={maNhanKhau}
                    sx={{ "& .MuiInputBase-root": { height: 60 }, input: { border: "none" }, gridColumn: "span 4" }}>
                    {dataNk.map((nhanKhau, index) => {
                      return <MenuItem key={index} value={nhanKhau.maNhanKhau}>{nhanKhau?.canCuocCongDan + ' - ' + nhanKhau?.hoTen}</MenuItem>
                    })}
                  </TextField>
                  <TextField
                    fullWidth
                    variant="filled"
                    type="text"
                    label="Lý do"
                    onBlur={handleBlur}
                    onChange={handleChange}
                    value={values.lyDo}
                    name="lyDo"
                    error={!!touched.lyDo && !!errors.lyDo}
                    helperText={touched.lyDo && errors.lyDo}
                    sx={{ "& .MuiInputBase-root": { height: 60 }, input: { border: "none" }, gridColumn: "span 4" }}
                  />
                  <LocalizationProvider dateAdapter={AdapterDayjs}>
                    <DesktopDatePicker label="Ngày bắt đầu"
                      inputFormat="DD/MM/YYYY"
                      onChange={handleOnChange}
                      value={date}
                      renderInput={(params) => <TextField style={{ width: 150 }} {...params} />}>
                    </DesktopDatePicker>
                  </LocalizationProvider>
                </Box>
                <Box display="flex" justifyContent="end" mt="20px" >
                  <Button
                    type="submit" color="secondary" variant="contained" startIcon={<SaveAsIcon />}>
                    Lưu
                  </Button>
                </Box>
              </form>
            )}
          </Formik>
        </Box>
      </DialogContent>
    </Dialog>
  );
};
const checkoutSchema = yup.object().shape({
  lyDo: yup.string().required("Bạn chưa điền thông tin"),
});
export default RegisterAbsent;


