import { KiemKeDatDaiTemplatePage } from './app.po';

describe('KiemKeDatDai App', function() {
  let page: KiemKeDatDaiTemplatePage;

  beforeEach(() => {
    page = new KiemKeDatDaiTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
