
const rules = {
  tenancyName: [{ required: true, message: 'Vui lòng điền đầy đủ thông tin' }],
  name: [{ required: true, message: 'Vui lòng điền đầy đủ thông tin' }],
  adminEmailAddress: [
    { required: true, message: 'Vui lòng điền đầy đủ thông tin' },
    {
      type: 'email',
      message: 'The input is not valid E-mail!',
    }
  ],
};

export default rules;
