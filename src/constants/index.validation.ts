
const rules = {
    required: [
        {
            required: true,
            message: "Vui lòng điền đầy đủ thông tin",
        },
    ],

    numberOnly: {
        pattern: RegExp('^[0-9]+$'),
        message: 'Vui lòng chỉ nhập kí tự số',
    },
    requiredAndNumberOnly: [
        {
            required: true,
            message: "Vui lòng điền đầy đủ thông tin",
        },
        {
            pattern: RegExp('^[0-9]+$'),
            message: 'Vui lòng chỉ nhập kí tự số',
        }
    ]

};

export default rules;
