
const rules = {
  name: [{ required: true, message: 'Vui lòng điền đầy đủ thông tin' }],
  surname: [{ required: true, message: 'Vui lòng điền đầy đủ thông tin' }],
  userName: [{ required: true, message: 'Vui lòng điền đầy đủ thông tin' }],
  emailAddress: [
    { required: true, message: 'Vui lòng điền đầy đủ thông tin' },
    {
      type: 'email',
      message: 'Địa chỉ email không đúng định dạng',
    },
  ],
};

export default rules;
