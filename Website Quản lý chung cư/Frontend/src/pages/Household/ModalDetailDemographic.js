import { Box, Typography, Dialog, DialogTitle, DialogContent, IconButton } from "@mui/material";
import CloseIcon from '@mui/icons-material/Close';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import moment from "moment";
const ModalDetailDemographic = ({ memberData, onClose }) => {
  return (
    <Dialog open={true} maxWidth="lg" style={{ backgroundColor: "transparent" }}>
      <DialogTitle>
        <div style={{ display: 'flex' }}>
          <Typography variant="h6" component="div" style={{ flexGrow: 1, fontSize: 20, fontWeight: "bold" }}>
            {"Thông tin thành viên"}
          </Typography>
          <IconButton aria-label="close" onClick={() => {
            onClose && onClose();
          }}>
            <CloseIcon></CloseIcon>
          </IconButton>
        </div>
      </DialogTitle>
      <DialogContent dividers>
        <Box m="20px">
          <table className="custom-table">
            <thead>
              <tr>
                <th>Họ tên</th>
                <th>Căn cước công dân</th>
                <th>Ngày sinh</th>
                <th>Nơi sinh</th>
                <th>Nghề nghiệp</th>
                <th>Quan hệ</th>
                <th>Trạng thái</th>
              </tr>
            </thead>
            <tbody>
              {memberData.map((row, index) => {
                return <tr key={index}>
                  <td>{row.hoTen}</td>
                  <td>{row.canCuocCongDan}</td>
                  <td>{moment(row.ngaySinh).format('YYYY-MM-DD')}</td>
                  <td>{row.noiSinh}</td>
                  <td>{row.ngheNghiep}</td>
                  <td>{row.quanHe}</td>
                  <td>
                    {row.trangThai === 1 ? 'Còn sống' : 'Đã mất'}
                  </td>
                </tr>
              })}
            </tbody>
          </table>
        </Box>
        <ToastContainer />
      </DialogContent>
    </Dialog>
  );
};
export default ModalDetailDemographic;


