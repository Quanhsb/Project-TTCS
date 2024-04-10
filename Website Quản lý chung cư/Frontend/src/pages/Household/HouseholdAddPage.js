import { Box, Button, TextField } from "@mui/material";
import { Formik, Field } from "formik";
import * as yup from "yup";
import useMediaQuery from "@mui/material/useMediaQuery";
import Header from "../../components/Header";
import householdService from "../../Services/API/householdService";
import CustomSelect from "../../components/CustomSelect";
import axios from "../../setups/custom_axios";
import { useState, useEffect } from "react";
import SaveAsIcon from '@mui/icons-material/SaveAs';
import { useHistory } from "react-router-dom";
import { toast } from 'react-toastify';
import { DesktopDatePicker, LocalizationProvider } from "@mui/x-date-pickers";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import 'react-toastify/dist/ReactToastify.css';
import dayjs from "dayjs";
const HouseholdAddPage = () => {
  const isNonMobile = useMediaQuery("(min-width:600px)");
  const history = useHistory();
  const [dataNhanKhau, setDataNhanKhau] = useState([]);
  const [ngayCap, setNgayCap] = useState();
  const handleSubmit = (values) => {
    if (window.confirm("Bạn chắc chắn muốn lưu?") === true) {
      householdService.addHouseHold({
        "maHoKhau": values.maHoKhau,
        "diaChiThuongTru": values.diaChiThuongTru,
        "noiCap": values.noiCap,
        "ngayCap": dayjs(ngayCap).format('YYYY-MM-DD'),
        "danhSachNhanKhau": values.danhSachNhanKhau
      }).then((result) => {
        const maHoKhau = result.data.maHoKhau;
        toast(result.message);
        history.push(`/${maHoKhau}/edit`);
      }).catch(e => {
        toast(e?.response?.data?.reason ?? e?.response?.data?.message ?? "Có lỗi xảy ra");
      })
    }
  }
  useEffect(
    async () => {
      try {
        const response = await axios.get(`/nhan-khau/danh-sach-nhan-khau-chua-co-ho-khau`)
        const datas = response.data;
        const datamap = datas.map((data) => {
          const label = data.hoTen + " " + data.maNhanKhau
          return {
            label,
            value: data.maNhanKhau
          }
        })
        setDataNhanKhau(datamap);
      } catch (error) {
        console.log(error);
      }
    }, []
  )
  return (
    <Box m="20px">
      <Header title="Đăng ký hộ khẩu" />
      <Formik
        onSubmit={(values) => handleSubmit(values)}
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
              }}
            >
              <TextField
                fullWidth
                variant="filled"
                type="text"
                label="Mã hộ khẩu"
                onBlur={handleBlur}
                onChange={handleChange}
                value={values.maHoKhau}
                name="maHoKhau"
                error={!!touched.maHoKhau && !!errors.maHoKhau}
                helperText={touched.maHoKhau && errors.maHoKhau}
                sx={{ "& .MuiInputBase-root": { height: 60 }, input: { border: "none" }, gridColumn: "span 4" }}
              />
              <TextField
                fullWidth
                variant="filled"
                type="text"
                label="Địa chỉ thường trú"
                onBlur={handleBlur}
                onChange={handleChange}
                value={values.diaChiThuongTru}
                name="diaChiThuongTru"
                error={!!touched.diaChiThuongTru && !!errors.diaChiThuongTru}
                helperText={touched.diaChiThuongTru && errors.diaChiThuongTru}
                sx={{ "& .MuiInputBase-root": { height: 60 }, input: { border: "none" }, gridColumn: "span 4" }}
              />
              <TextField
                fullWidth
                variant="filled"
                type="text"
                label="Nơi cấp"
                onBlur={handleBlur}
                onChange={handleChange}
                value={values.noiCap}
                name="noiCap"
                error={!!touched.noiCap && !!errors.noiCap}
                helperText={touched.noiCap && errors.noiCap}
                sx={{ "& .MuiInputBase-root": { height: 60 }, input: { border: "none" }, gridColumn: "span 2" }}
              />
              <LocalizationProvider dateAdapter={AdapterDayjs}>
                <DesktopDatePicker label="Ngày cấp"
                  inputFormat="DD/MM/YYYY"
                  onChange={setNgayCap}
                  value={ngayCap}
                  renderInput={(params) => <TextField {...params} />}>
                </DesktopDatePicker>
              </LocalizationProvider>
              <br />
              <Field
                fullWidth
                className="custom-select"
                name="danhSachNhanKhau"
                options={dataNhanKhau}
                component={CustomSelect}
                placeholder="Danh sách mã nhân khẩu "
                isMulti={true}
                sx={{ width: "400px" }}
              />
            </Box>
            <Box display="flex" justifyContent="end" mt="20px">
              <Button type="submit" color="info" variant="contained" startIcon={<SaveAsIcon />}>
                Lưu
              </Button>
            </Box>
          </form>
        )}
      </Formik>
    </Box>
  );
};
const checkoutSchema = yup.object().shape({
  maHoKhau: yup.string(),
  diaChiThuongTru: yup.string().required("Bạn chưa điền thông tin"),
  noiCap: yup.string().required("Bạn chưa điền thông tin"),
  ngayCap: yup.string().required("Bạn chưa điền thông tin"),
  danhSachNhanKhau: yup.array().required("Bạn chưa điền thông tin")
});
let initialValues = {
  maHoKhau: "",
  diaChiThuongTru: "",
  noiCap: "",
  ngayCap: new Date(),
  danhSachNhanKhau: [],
};
export default HouseholdAddPage;
