import React from 'react'

export default function FloatingChat() {
    return (
        <div style={styles.container}>
            <a
                href="https://zalo.me/0366101028"
                target="_blank"
                rel="noopener noreferrer"
                style={styles.contactItem}
            >

                <img
                    src="https://upload.wikimedia.org/wikipedia/commons/9/91/Icon_of_Zalo.svg"
                    alt="Zalo Hỗ trợ"
                    style={styles.icon}
                />
                <div style={styles.textContainer}>
                    <span style={styles.text}>Zalo Hỗ trợ</span>
                    <span style={styles.time}>(8h-18h)</span>
                </div>
            </a>

            <a
                href="tel:0366101028"
                style={styles.contactItem}
            >

                <img
                    src="https://icons.veryicon.com/png/o/education-technology/smart-technology-icon/smartphone-16.png"
                    alt="Phone"
                    style={styles.icon}
                />
                <div style={styles.textContainer}>
                    <span style={styles.text}>0366.101.028</span>
                    <span style={styles.time}>(8h-18h)</span>
                </div>
            </a>
        </div>
    );
}
const styles: any = {
    container: {
        position: 'fixed',
        right: '20px',
        top: '75%',
        display: 'flex',
        flexDirection: 'column',
        gap: '10px',
        zIndex: 1000,
    },
    contactItem: {
        display: 'flex',
        alignItems: 'center',
        backgroundColor: '#fff',
        borderRadius: '12px',
        padding: '8px 12px',
        boxShadow: '0 4px 8px rgba(0,0,0,0.1)',
        minWidth: '180px',
    },
    icon: {
        width: '24px',
        height: '24px',
        marginRight: '8px',
        objectFit: 'contain',
    },
    textContainer: {
        display: 'flex',
        flexDirection: 'column',
    },
    text: {
        fontWeight: '600',
        fontSize: '12px',
        color: '#333',
    },
    time: {
        fontSize: '8px',
        color: '#888',
    }
};