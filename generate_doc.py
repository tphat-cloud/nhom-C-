# -*- coding: utf-8 -*-
"""
generate_doc.py
================
Sinh báo cáo đồ án cuối kỳ dưới dạng .docx theo chuẩn trình bày đại học:
    - Times New Roman, nội dung 13pt, giãn dòng 1.5, Before/After 3pt
    - Lề: trên 2.0 cm, dưới 2.0 cm, trái 3.0 cm, phải 2.0 cm
    - Heading 1 (16pt bold, chữ hoa) / Heading 2 (14pt bold) / Heading 3 (13pt bold italic)
    - Bảng biểu: viền navy #1B365D, dòng tiêu đề nền navy chữ trắng in đậm
    - Mục lục, danh mục bảng biểu, danh mục hình ảnh đều là field tự động của Word

Cách chạy:
    python generate_doc.py
    python generate_doc.py --out BaoCao_Khac.docx

Yêu cầu: python-docx (pip install python-docx)
"""

from __future__ import annotations

import argparse
import os
import sys

from docx import Document
from docx.enum.section import WD_SECTION
from docx.enum.style import WD_STYLE_TYPE
from docx.enum.table import WD_ALIGN_VERTICAL, WD_ROW_HEIGHT_RULE, WD_TABLE_ALIGNMENT
from docx.enum.text import WD_ALIGN_PARAGRAPH, WD_BREAK, WD_LINE_SPACING
from docx.oxml import OxmlElement
from docx.oxml.ns import qn
from docx.shared import Cm, Pt, RGBColor

import content_chuong1
import content_chuong2
import content_chuong3

# ----------------------------------------------------------------------------
# Hằng số định dạng
# ----------------------------------------------------------------------------
FONT = "Times New Roman"
MONO_FONT = "Consolas"
NAVY = "1B365D"
NAVY_RGB = RGBColor(0x1B, 0x36, 0x5D)
BLACK = RGBColor(0, 0, 0)
GREY_LIGHT = "F2F5F9"
BODY_SIZE = 13
TABLE_SIZE = 12
CONTENT_WIDTH_CM = 16.0  # 21 - 3 - 2

DEFAULT_OUT = "BaoCao_DoAn_QuanLyCuaHangThoiTrang.docx"


# ----------------------------------------------------------------------------
# Tiện ích cấp thấp
# ----------------------------------------------------------------------------
def set_run_font(run, size=None, bold=None, italic=None, name=FONT, color=None):
    run.font.name = name
    if size is not None:
        run.font.size = Pt(size)
    if bold is not None:
        run.font.bold = bold
    if italic is not None:
        run.font.italic = italic
    if color is not None:
        run.font.color.rgb = color

    rpr = run._element.get_or_add_rPr()
    rfonts = rpr.find(qn("w:rFonts"))
    if rfonts is None:
        rfonts = OxmlElement("w:rFonts")
        rpr.append(rfonts)
    for attr in ("w:ascii", "w:hAnsi", "w:cs", "w:eastAsia"):
        rfonts.set(qn(attr), name)


def style_set_font(style, size, bold=False, italic=False, name=FONT, color=BLACK):
    style.font.name = name
    style.font.size = Pt(size)
    style.font.bold = bold
    style.font.italic = italic
    style.font.color.rgb = color

    rpr = style.element.get_or_add_rPr()
    rfonts = rpr.find(qn("w:rFonts"))
    if rfonts is None:
        rfonts = OxmlElement("w:rFonts")
        rpr.append(rfonts)
    for attr in ("w:ascii", "w:hAnsi", "w:cs", "w:eastAsia"):
        rfonts.set(qn(attr), name)


def shade_cell(cell, hex_color):
    tc_pr = cell._tc.get_or_add_tcPr()
    shd = OxmlElement("w:shd")
    shd.set(qn("w:val"), "clear")
    shd.set(qn("w:color"), "auto")
    shd.set(qn("w:fill"), hex_color)
    tc_pr.append(shd)


def set_table_borders(table, color=NAVY, size=6):
    tbl_pr = table._tbl.tblPr
    borders = OxmlElement("w:tblBorders")
    for edge in ("top", "left", "bottom", "right", "insideH", "insideV"):
        element = OxmlElement(f"w:{edge}")
        element.set(qn("w:val"), "single")
        element.set(qn("w:sz"), str(size))
        element.set(qn("w:space"), "0")
        element.set(qn("w:color"), color)
        borders.append(element)
    tbl_pr.append(borders)


def repeat_header_row(row):
    tr_pr = row._tr.get_or_add_trPr()
    header = OxmlElement("w:tblHeader")
    header.set(qn("w:val"), "true")
    tr_pr.append(header)


def keep_row_together(row):
    tr_pr = row._tr.get_or_add_trPr()
    cant_split = OxmlElement("w:cantSplit")
    cant_split.set(qn("w:val"), "true")
    tr_pr.append(cant_split)


def add_field(paragraph, instruction, placeholder="", size=12, bold=False):
    """Chèn field code Word (TOC, PAGE, SEQ...) vào paragraph."""
    run = paragraph.add_run()
    fld_begin = OxmlElement("w:fldChar")
    fld_begin.set(qn("w:fldCharType"), "begin")
    run._r.append(fld_begin)

    run_instr = paragraph.add_run()
    instr = OxmlElement("w:instrText")
    instr.set(qn("xml:space"), "preserve")
    instr.text = f" {instruction} "
    run_instr._r.append(instr)

    run_sep = paragraph.add_run()
    fld_sep = OxmlElement("w:fldChar")
    fld_sep.set(qn("w:fldCharType"), "separate")
    run_sep._r.append(fld_sep)

    run_text = paragraph.add_run(placeholder)
    set_run_font(run_text, size=size, bold=bold)

    run_end = paragraph.add_run()
    fld_end = OxmlElement("w:fldChar")
    fld_end.set(qn("w:fldCharType"), "end")
    run_end._r.append(fld_end)

    for item in (run, run_instr, run_sep, run_end):
        set_run_font(item, size=size)


def set_cell_text(cell, text, size=TABLE_SIZE, bold=False, color=BLACK, align=WD_ALIGN_PARAGRAPH.LEFT):
    cell.vertical_alignment = WD_ALIGN_VERTICAL.CENTER
    lines = str(text).split("\n")
    cell.text = ""
    paragraph = cell.paragraphs[0]
    paragraph.alignment = align
    paragraph.paragraph_format.space_before = Pt(2)
    paragraph.paragraph_format.space_after = Pt(2)
    paragraph.paragraph_format.line_spacing = 1.15
    paragraph.paragraph_format.first_line_indent = Cm(0)
    run = paragraph.add_run(lines[0])
    set_run_font(run, size=size, bold=bold, color=color)

    for line in lines[1:]:
        extra = cell.add_paragraph()
        extra.alignment = align
        extra.paragraph_format.space_before = Pt(0)
        extra.paragraph_format.space_after = Pt(2)
        extra.paragraph_format.line_spacing = 1.15
        extra.paragraph_format.first_line_indent = Cm(0)
        run = extra.add_run(line)
        set_run_font(run, size=size, bold=bold, color=color)


# ----------------------------------------------------------------------------
# Thiết lập tài liệu
# ----------------------------------------------------------------------------
def setup_document():
    doc = Document()

    section = doc.sections[0]
    section.page_width = Cm(21)
    section.page_height = Cm(29.7)
    section.top_margin = Cm(2.0)
    section.bottom_margin = Cm(2.0)
    section.left_margin = Cm(3.0)
    section.right_margin = Cm(2.0)
    section.header_distance = Cm(1.0)
    section.footer_distance = Cm(1.0)

    # Không đánh số trang ở trang bìa
    section.different_first_page_header_footer = True

    styles = doc.styles

    normal = styles["Normal"]
    style_set_font(normal, BODY_SIZE)
    pf = normal.paragraph_format
    pf.line_spacing_rule = WD_LINE_SPACING.ONE_POINT_FIVE
    pf.space_before = Pt(3)
    pf.space_after = Pt(3)
    pf.alignment = WD_ALIGN_PARAGRAPH.JUSTIFY
    pf.widow_control = True

    h1 = styles["Heading 1"]
    style_set_font(h1, 16, bold=True)
    h1.paragraph_format.alignment = WD_ALIGN_PARAGRAPH.CENTER
    h1.paragraph_format.space_before = Pt(0)
    h1.paragraph_format.space_after = Pt(12)
    h1.paragraph_format.page_break_before = True
    h1.paragraph_format.keep_with_next = True
    h1.paragraph_format.line_spacing_rule = WD_LINE_SPACING.ONE_POINT_FIVE

    h2 = styles["Heading 2"]
    style_set_font(h2, 14, bold=True)
    h2.paragraph_format.alignment = WD_ALIGN_PARAGRAPH.LEFT
    h2.paragraph_format.space_before = Pt(12)
    h2.paragraph_format.space_after = Pt(6)
    h2.paragraph_format.keep_with_next = True
    h2.paragraph_format.page_break_before = False
    h2.paragraph_format.line_spacing_rule = WD_LINE_SPACING.ONE_POINT_FIVE

    h3 = styles["Heading 3"]
    style_set_font(h3, 13, bold=True, italic=True)
    h3.paragraph_format.alignment = WD_ALIGN_PARAGRAPH.LEFT
    h3.paragraph_format.space_before = Pt(9)
    h3.paragraph_format.space_after = Pt(3)
    h3.paragraph_format.keep_with_next = True
    h3.paragraph_format.page_break_before = False
    h3.paragraph_format.line_spacing_rule = WD_LINE_SPACING.ONE_POINT_FIVE

    # Danh sách
    for name in ("List Bullet", "List Bullet 2", "List Number"):
        try:
            style = styles[name]
        except KeyError:
            continue
        style_set_font(style, BODY_SIZE)
        style.paragraph_format.line_spacing_rule = WD_LINE_SPACING.ONE_POINT_FIVE
        style.paragraph_format.space_before = Pt(3)
        style.paragraph_format.space_after = Pt(3)
        style.paragraph_format.alignment = WD_ALIGN_PARAGRAPH.JUSTIFY

    # Caption dùng cho bảng / hình (tách 2 style để Word làm 2 danh mục riêng)
    caption = styles["Caption"]
    style_set_font(caption, 12, italic=True, color=NAVY_RGB)
    caption.paragraph_format.alignment = WD_ALIGN_PARAGRAPH.CENTER
    caption.paragraph_format.space_before = Pt(6)
    caption.paragraph_format.space_after = Pt(6)
    caption.paragraph_format.line_spacing_rule = WD_LINE_SPACING.SINGLE

    for name in ("CaptionBang", "CaptionHinh"):
        new_style = styles.add_style(name, WD_STYLE_TYPE.PARAGRAPH)
        new_style.base_style = caption
        new_style.quick_style = False

    # Tiêu đề phần đầu báo cáo (không nằm trong mục lục)
    front = styles.add_style("FrontHeading", WD_STYLE_TYPE.PARAGRAPH)
    front.base_style = styles["Heading 1"]
    front.paragraph_format.page_break_before = True
    front.paragraph_format.space_after = Pt(12)
    style_set_font(front, 16, bold=True)

    # Khối mã nguồn / câu lệnh
    code = styles.add_style("CodeBlock", WD_STYLE_TYPE.PARAGRAPH)
    code.base_style = styles["Normal"]
    style_set_font(code, 10.5, name=MONO_FONT)
    code.paragraph_format.line_spacing_rule = WD_LINE_SPACING.SINGLE
    code.paragraph_format.space_before = Pt(6)
    code.paragraph_format.space_after = Pt(6)
    code.paragraph_format.left_indent = Cm(0.6)
    code.paragraph_format.alignment = WD_ALIGN_PARAGRAPH.LEFT

    add_footer_page_number(section)
    return doc


def add_footer_page_number(section):
    footer = section.footer
    paragraph = footer.paragraphs[0]
    paragraph.alignment = WD_ALIGN_PARAGRAPH.CENTER
    paragraph.paragraph_format.space_before = Pt(0)
    paragraph.paragraph_format.space_after = Pt(0)
    run = paragraph.add_run("Trang ")
    set_run_font(run, size=11)
    add_field(paragraph, "PAGE", "1", size=11)


# ----------------------------------------------------------------------------
# Các khối nội dung
# ----------------------------------------------------------------------------
class ReportBuilder:
    def __init__(self, doc):
        self.doc = doc
        self.table_count = 0
        self.figure_count = 0

    # --- khối văn bản -------------------------------------------------------
    def h1(self, text):
        paragraph = self.doc.add_paragraph(text.upper(), style="Heading 1")
        for run in paragraph.runs:
            set_run_font(run, size=16, bold=True, color=BLACK)
        return paragraph

    def h2(self, text):
        paragraph = self.doc.add_paragraph(text, style="Heading 2")
        for run in paragraph.runs:
            set_run_font(run, size=14, bold=True, color=BLACK)
        return paragraph

    def h3(self, text):
        paragraph = self.doc.add_paragraph(text, style="Heading 3")
        for run in paragraph.runs:
            set_run_font(run, size=13, bold=True, italic=True, color=BLACK)
        return paragraph

    def p(self, text, indent=True, align=WD_ALIGN_PARAGRAPH.JUSTIFY, italic=False, bold=False):
        paragraph = self.doc.add_paragraph(style="Normal")
        paragraph.alignment = align
        if indent:
            paragraph.paragraph_format.first_line_indent = Cm(1.0)
        run = paragraph.add_run(text)
        set_run_font(run, size=BODY_SIZE, italic=italic, bold=bold)
        return paragraph

    def note(self, text):
        paragraph = self.doc.add_paragraph(style="Normal")
        paragraph.alignment = WD_ALIGN_PARAGRAPH.JUSTIFY
        paragraph.paragraph_format.left_indent = Cm(0.8)
        paragraph.paragraph_format.first_line_indent = Cm(0)
        run = paragraph.add_run(text)
        set_run_font(run, size=12, italic=True, color=NAVY_RGB)
        return paragraph

    def bullets(self, items, level=1):
        style = "List Bullet" if level == 1 else "List Bullet 2"
        for item in items:
            paragraph = self.doc.add_paragraph(style=style)
            paragraph.paragraph_format.first_line_indent = Cm(0)
            run = paragraph.add_run(item)
            set_run_font(run, size=BODY_SIZE)

    def numbers(self, items):
        for item in items:
            paragraph = self.doc.add_paragraph(style="List Number")
            paragraph.paragraph_format.first_line_indent = Cm(0)
            run = paragraph.add_run(item)
            set_run_font(run, size=BODY_SIZE)

    def code(self, text):
        paragraphs = text.strip("\n").split("\n")
        for index, line in enumerate(paragraphs):
            paragraph = self.doc.add_paragraph(style="CodeBlock")
            if index == 0:
                paragraph.paragraph_format.space_before = Pt(6)
            run = paragraph.add_run(line if line else " ")
            set_run_font(run, size=10.5, name=MONO_FONT)

    def pagebreak(self):
        self.doc.add_paragraph().add_run().add_break(WD_BREAK.PAGE)

    # --- caption ------------------------------------------------------------
    def caption(self, text, kind, keep_with_next=False):
        style = "CaptionBang" if kind == "table" else "CaptionHinh"
        paragraph = self.doc.add_paragraph(style=style)
        paragraph.paragraph_format.keep_with_next = keep_with_next
        prefix = "Bảng " if kind == "table" else "Hình "
        run = paragraph.add_run(prefix + text)
        set_run_font(run, size=12, italic=True, color=NAVY_RGB)
        return paragraph

    # --- bảng ---------------------------------------------------------------
    def table(self, spec):
        caption = spec.get("caption")
        cols = spec["cols"]
        rows = spec["rows"]
        widths = spec.get("widths")
        font_size = spec.get("font", TABLE_SIZE)
        bold_first = spec.get("bold_first_col", False)
        aligns = spec.get("aligns")

        if caption:
            self.caption(caption, "table", keep_with_next=True)

        table = self.doc.add_table(rows=1, cols=len(cols))
        table.style = "Table Grid"
        table.alignment = WD_TABLE_ALIGNMENT.CENTER
        table.autofit = False
        set_table_borders(table)

        if widths is None:
            widths = [CONTENT_WIDTH_CM / len(cols)] * len(cols)
        scale = CONTENT_WIDTH_CM / sum(widths)
        widths = [w * scale for w in widths]

        header = table.rows[0]
        repeat_header_row(header)
        keep_row_together(header)
        for index, column in enumerate(cols):
            cell = header.cells[index]
            cell.width = Cm(widths[index])
            shade_cell(cell, NAVY)
            set_cell_text(cell, column, size=font_size, bold=True,
                          color=RGBColor(0xFF, 0xFF, 0xFF),
                          align=WD_ALIGN_PARAGRAPH.CENTER)

        for row_values in rows:
            row = table.add_row()
            keep_row_together(row)
            for index, value in enumerate(row_values):
                cell = row.cells[index]
                cell.width = Cm(widths[index])
                is_bold = bold_first and index == 0
                alignment = WD_ALIGN_PARAGRAPH.LEFT
                if aligns and index < len(aligns):
                    alignment = {"c": WD_ALIGN_PARAGRAPH.CENTER,
                                 "r": WD_ALIGN_PARAGRAPH.RIGHT,
                                 "l": WD_ALIGN_PARAGRAPH.LEFT}[aligns[index]]
                set_cell_text(cell, value, size=font_size, bold=is_bold, align=alignment)

        # khoảng trống sau bảng
        spacer = self.doc.add_paragraph()
        spacer.paragraph_format.space_before = Pt(0)
        spacer.paragraph_format.space_after = Pt(6)
        spacer.paragraph_format.line_spacing = 1.0
        run = spacer.add_run("")
        set_run_font(run, size=6)
        return table

    # --- khung hình ---------------------------------------------------------
    def figure(self, spec):
        caption = spec.get("caption", "")
        source = spec.get("file", "")
        note = spec.get("note", "")
        height = spec.get("height", 6.0)

        box = self.doc.add_table(rows=1, cols=1)
        box.style = "Table Grid"
        box.alignment = WD_TABLE_ALIGNMENT.CENTER
        box.autofit = False
        set_table_borders(box, color=NAVY, size=6)

        row = box.rows[0]
        row.height = Cm(height)
        row.height_rule = WD_ROW_HEIGHT_RULE.EXACTLY

        cell = row.cells[0]
        cell.width = Cm(CONTENT_WIDTH_CM)
        shade_cell(cell, GREY_LIGHT)
        cell.vertical_alignment = WD_ALIGN_VERTICAL.CENTER
        cell.text = ""

        first = cell.paragraphs[0]
        first.alignment = WD_ALIGN_PARAGRAPH.CENTER
        first.paragraph_format.space_before = Pt(0)
        first.paragraph_format.space_after = Pt(0)
        run = first.add_run("[ Khung chèn sơ đồ ]")
        set_run_font(run, size=11, bold=True, color=NAVY_RGB)

        second = cell.add_paragraph()
        second.alignment = WD_ALIGN_PARAGRAPH.CENTER
        second.paragraph_format.space_before = Pt(3)
        second.paragraph_format.space_after = Pt(0)
        run = second.add_run(f"Mã nguồn Mermaid: {source}")
        set_run_font(run, size=10, italic=True, color=NAVY_RGB)

        if note:
            third = cell.add_paragraph()
            third.alignment = WD_ALIGN_PARAGRAPH.CENTER
            third.paragraph_format.space_before = Pt(3)
            third.paragraph_format.space_after = Pt(0)
            run = third.add_run(note)
            set_run_font(run, size=9.5, italic=True, color=RGBColor(0x55, 0x55, 0x55))

        spacer = self.doc.add_paragraph()
        spacer.paragraph_format.space_before = Pt(0)
        spacer.paragraph_format.space_after = Pt(0)
        spacer.paragraph_format.line_spacing = 1.0
        spacer.add_run("").font.size = Pt(4)

        if caption:
            self.caption(caption, "figure")
        self.figure_count += 1

    # --- điều phối ----------------------------------------------------------
    def render(self, blocks):
        for kind, value in blocks:
            if kind == "h1":
                self.h1(value)
            elif kind == "h2":
                self.h2(value)
            elif kind == "h3":
                self.h3(value)
            elif kind == "p":
                self.p(value)
            elif kind == "p_noindent":
                self.p(value, indent=False)
            elif kind == "note":
                self.note(value)
            elif kind == "bul":
                self.bullets(value)
            elif kind == "bul2":
                self.bullets(value, level=2)
            elif kind == "num":
                self.numbers(value)
            elif kind == "code":
                self.code(value)
            elif kind == "table":
                self.table(value)
            elif kind == "figure":
                self.figure(value)
            elif kind == "pb":
                self.pagebreak()
            else:
                raise ValueError(f"Khối nội dung không hỗ trợ: {kind}")


# ----------------------------------------------------------------------------
# Phần đầu báo cáo
# ----------------------------------------------------------------------------
def cover_page(doc):
    def centered(text, size, bold=False, italic=False, space_before=0, space_after=0, upper=False):
        paragraph = doc.add_paragraph()
        paragraph.alignment = WD_ALIGN_PARAGRAPH.CENTER
        paragraph.paragraph_format.first_line_indent = Cm(0)
        paragraph.paragraph_format.space_before = Pt(space_before)
        paragraph.paragraph_format.space_after = Pt(space_after)
        paragraph.paragraph_format.line_spacing = 1.2
        run = paragraph.add_run(text.upper() if upper else text)
        set_run_font(run, size=size, bold=bold, italic=italic)
        return paragraph

    centered("TRƯỜNG ĐẠI HỌC SÀI GÒN", 16, bold=True, space_before=6, space_after=2)
    centered("KHOA CÔNG NGHỆ THÔNG TIN", 14, bold=True, space_after=6)
    centered("───────  ✦  ───────", 12, space_after=18)

    centered("BÁO CÁO ĐỒ ÁN CUỐI KỲ", 18, bold=True, space_before=12, space_after=4)
    centered("Học phần: Ngôn ngữ lập trình C#", 13, italic=True, space_after=24)

    title_table = doc.add_table(rows=1, cols=1)
    title_table.alignment = WD_TABLE_ALIGNMENT.CENTER
    title_table.autofit = False
    set_table_borders(title_table, color=NAVY, size=12)
    cell = title_table.rows[0].cells[0]
    cell.width = Cm(14.5)
    shade_cell(cell, GREY_LIGHT)
    cell.text = ""
    first = cell.paragraphs[0]
    first.alignment = WD_ALIGN_PARAGRAPH.CENTER
    first.paragraph_format.space_before = Pt(14)
    first.paragraph_format.space_after = Pt(4)
    run = first.add_run("HỆ THỐNG QUẢN LÝ VÀ BÁN HÀNG\nTHỜI TRANG")
    set_run_font(run, size=20, bold=True, color=NAVY_RGB)

    second = cell.add_paragraph()
    second.alignment = WD_ALIGN_PARAGRAPH.CENTER
    second.paragraph_format.space_before = Pt(2)
    second.paragraph_format.space_after = Pt(14)
    run = second.add_run(
        "(Ứng dụng quản trị Desktop C# WinForms song song với Website ASP.NET Core MVC,\n"
        "cùng khai thác cơ sở dữ liệu SQL Server 2022)"
    )
    set_run_font(run, size=11.5, italic=True)

    centered("", 12, space_after=10)
    centered("NHÓM SINH VIÊN THỰC HIỆN", 13, bold=True, space_before=18, space_after=6)

    member_table = doc.add_table(rows=1, cols=4)
    member_table.alignment = WD_TABLE_ALIGNMENT.CENTER
    member_table.autofit = False
    set_table_borders(member_table)
    headers = ["STT", "Họ và tên", "Mã số sinh viên", "Vai trò trong nhóm"]
    widths = [1.4, 5.6, 4.2, 4.8]
    header = member_table.rows[0]
    repeat_header_row(header)
    for index, text in enumerate(headers):
        current = header.cells[index]
        current.width = Cm(widths[index])
        shade_cell(current, NAVY)
        set_cell_text(current, text, size=11, bold=True,
                      color=RGBColor(0xFF, 0xFF, 0xFF), align=WD_ALIGN_PARAGRAPH.CENTER)

    members = [
        ("1", "……………………………………", "………………", "Nhóm trưởng"),
        ("2", "……………………………………", "………………", "Thành viên"),
        ("3", "……………………………………", "………………", "Thành viên"),
    ]
    for values in members:
        row = member_table.add_row()
        for index, text in enumerate(values):
            current = row.cells[index]
            current.width = Cm(widths[index])
            set_cell_text(current, text, size=11, align=WD_ALIGN_PARAGRAPH.CENTER)

    centered("Giảng viên hướng dẫn: TS. Đỗ Như Tài", 13, bold=True, space_before=22, space_after=6)
    centered("Thành phố Hồ Chí Minh, tháng 10 năm 2026", 13, space_before=30)


def front_matter(doc, builder):
    # ---- Lời cảm ơn ----
    heading = doc.add_paragraph("LỜI CẢM ƠN", style="FrontHeading")
    heading.alignment = WD_ALIGN_PARAGRAPH.CENTER
    for run in heading.runs:
        set_run_font(run, size=16, bold=True)

    builder.p(
        "Trong quá trình thực hiện đồ án cuối kỳ môn Ngôn ngữ lập trình C#, nhóm chúng em đã nhận được "
        "sự hướng dẫn, góp ý và động viên rất quý báu từ quý thầy cô Khoa Công nghệ Thông tin, "
        "Trường Đại học Sài Gòn. Đặc biệt, nhóm xin bày tỏ lòng biết ơn sâu sắc đến Thầy Đỗ Như Tài – "
        "giảng viên hướng dẫn – người đã định hướng đề tài, chỉ ra những điểm chưa hợp lý trong cách "
        "tổ chức mã nguồn, hướng dẫn cách đặt câu hỏi đúng khi thiết kế cơ sở dữ liệu và luôn nhấn mạnh "
        "rằng một hệ thống tốt phải bảo vệ được tính toàn vẹn dữ liệu trước các tình huống bất thường."
    )
    builder.p(
        "Nhóm cũng xin cảm ơn quý thầy cô đã truyền đạt nền tảng lập trình hướng đối tượng, kỹ thuật "
        "truy vấn SQL và phương pháp phân tích thiết kế hệ thống. Những kiến thức đó là cơ sở để nhóm "
        "hiểu và vận dụng được các công nghệ .NET 8, Windows Forms, ASP.NET Core MVC và SQL Server "
        "trong một sản phẩm hoàn chỉnh thay vì những bài tập rời rạc."
    )
    builder.p(
        "Đồ án được xây dựng trong thời gian có hạn, dữ liệu khảo sát chủ yếu từ nhu cầu thực tế của "
        "các cửa hàng thời trang quy mô nhỏ, nên chắc chắn không tránh khỏi thiếu sót. Nhóm rất mong "
        "nhận được nhận xét của quý thầy cô để tiếp tục hoàn thiện sản phẩm và nâng cao năng lực "
        "kỹ thuật của từng thành viên."
    )
    builder.p("Nhóm sinh viên thực hiện", indent=False, align=WD_ALIGN_PARAGRAPH.RIGHT, italic=True)

    # ---- Phân công nhiệm vụ ----
    heading = doc.add_paragraph("BẢNG PHÂN CÔNG NHIỆM VỤ THÀNH VIÊN", style="FrontHeading")
    heading.alignment = WD_ALIGN_PARAGRAPH.CENTER
    for run in heading.runs:
        set_run_font(run, size=16, bold=True)

    builder.p(
        "Bảng phân công dưới đây xác định phạm vi công việc và sản phẩm bàn giao của từng thành viên. "
        "Cách chia việc theo chiều dọc (mỗi thành viên phụ trách trọn vẹn một nhóm chức năng từ cơ sở "
        "dữ liệu, xử lý nghiệp vụ đến giao diện) giúp giảm thời gian chờ đợi chéo và hạn chế việc "
        "nhiều người cùng sửa một tệp mã nguồn."
    )

    builder.table({
        "caption": "1: Phân công nhiệm vụ và sản phẩm bàn giao của các thành viên",
        "cols": ["Thành viên", "Phạm vi phụ trách", "Sản phẩm bàn giao", "Đánh giá"],
        "widths": [3.4, 6.4, 5.2, 1.8],
        "font": 11,
        "aligns": ["l", "l", "l", "c"],
        "rows": [
            ["Thành viên 1\n(Nhóm trưởng)",
             "Chương 1 và Chương 3.1 – 3.2\nKiến trúc tổng thể, quy trình Scrum, đặc tả Use Case, "
             "điều phối tích hợp hai phân hệ",
             "Sơ đồ kiến trúc, bộ đặc tả UC01 – UC04, kịch bản kiểm thử tích hợp",
             "Hoàn thành"],
            ["Thành viên 2",
             "Chương 2 – Phân hệ Desktop\nForm1, FormMain, FormDanhMuc, FormBienTheSanPham, FormDonHang, "
             "DatabaseConfig",
             "Mã nguồn WinForms, script CSDL_ShopQuanAo.sql, migration 01_harden_database.sql",
             "Hoàn thành"],
            ["Thành viên 3",
             "Chương 2.3 và Chương 3.3 – 3.4\nPhân hệ Web, thiết kế CSDL vật lý, thiết kế giao diện",
             "Controllers, Repository ADO.NET, Razor Views, tài liệu thiết kế CSDL và Screen Flow",
             "Hoàn thành"],
        ],
    })

    builder.p(
        "Ngoài phần việc riêng, cả nhóm cùng thực hiện các hoạt động chung: rà soát mã nguồn trước mỗi "
        "lần hợp nhất, kiểm thử chéo theo kịch bản đã thống nhất và hiệu đính báo cáo. Mã nguồn được "
        "quản lý tập trung trong một kho lưu trữ Git duy nhất, mỗi thành viên làm việc trên nhánh riêng "
        "và hợp nhất vào nhánh chính sau khi được nhóm trưởng xem lại."
    )
    builder.table({
        "caption": "2: Kế hoạch phối hợp và kiểm soát chất lượng của nhóm",
        "cols": ["Hoạt động", "Tần suất", "Công cụ / cách thực hiện", "Đầu ra"],
        "widths": [3.6, 2.6, 6.0, 4.0],
        "rows": [
            ["Họp tiến độ (Daily Scrum thu gọn)", "Hằng ngày, 15 phút",
             "Trao đổi trực tiếp và ghi chú trên bảng công việc chung",
             "Danh sách việc đã làm, việc sẽ làm, điểm đang vướng"],
            ["Rà soát mã nguồn", "Trước mỗi lần hợp nhất",
             "Đọc diff, chạy thử luồng nghiệp vụ bị ảnh hưởng",
             "Nhận xét và quyết định hợp nhất"],
            ["Kiểm thử chéo", "Cuối mỗi Sprint",
             "Kịch bản kiểm thử viết tay, chạy trên SQL Server thật trong Docker",
             "Bảng kết quả kiểm thử, danh sách lỗi cần sửa"],
            ["Cập nhật tài liệu", "Cuối mỗi Sprint",
             "Ghi lại quyết định thiết kế và lý do thay đổi",
             "Báo cáo, sơ đồ, ghi chú kỹ thuật"],
        ],
    })

    # ---- Mục lục ----
    heading = doc.add_paragraph("MỤC LỤC", style="FrontHeading")
    heading.alignment = WD_ALIGN_PARAGRAPH.CENTER
    for run in heading.runs:
        set_run_font(run, size=16, bold=True)

    builder.table({
        "caption": "3: Cấu trúc tổng thể của báo cáo",
        "cols": ["Chương", "Nội dung chính", "Sản phẩm đọc kèm"],
        "widths": [3.4, 8.6, 4.0],
        "rows": [
            ["Chương 1", "Tổng quan hệ thống: bối cảnh, mục tiêu, kiến trúc tổng thể, quy trình Scrum",
             "Hình 1.1 – 1.2, Bảng 1.1 – 1.4"],
            ["Chương 2", "Khảo sát công nghệ: nguyên lý thiết kế, kiến trúc hai phân hệ, phân tích mã nguồn",
             "Hình 2.1 – 2.2, Bảng 2.1 – 2.8"],
            ["Chương 3", "Phân tích và thiết kế: nghiệp vụ, Use Case, User Story, CSDL, giao diện",
             "Hình 3.1 – 3.10, Bảng 3.1 – 3.30"],
            ["Phụ lục", "Đối chiếu Use Case với mã nguồn, danh mục sơ đồ Mermaid, hướng dẫn render",
             "Toàn bộ thư mục diagrams/"],
        ],
    })

    paragraph = doc.add_paragraph()
    paragraph.paragraph_format.first_line_indent = Cm(0)
    paragraph.paragraph_format.space_before = Pt(6)
    add_field(paragraph, r'TOC \o "1-3" \h \z \u',
              "Mục lục tự động: bấm chuột phải vào vùng này và chọn Update Field (hoặc Ctrl+A rồi F9) "
              "để Word dựng lại mục lục kèm số trang.", size=11)

    # ---- Danh mục bảng biểu ----
    heading = doc.add_paragraph("DANH MỤC BẢNG BIỂU", style="FrontHeading")
    heading.alignment = WD_ALIGN_PARAGRAPH.CENTER
    for run in heading.runs:
        set_run_font(run, size=16, bold=True)

    paragraph = doc.add_paragraph()
    paragraph.paragraph_format.first_line_indent = Cm(0)
    paragraph.paragraph_format.space_before = Pt(6)
    add_field(paragraph, r'TOC \h \z \t "CaptionBang,1"',
              "Danh mục bảng biểu tự động: bấm Update Field để cập nhật danh sách và số trang.", size=11)

    # ---- Danh mục hình ảnh ----
    heading = doc.add_paragraph("DANH MỤC HÌNH ẢNH", style="FrontHeading")
    heading.alignment = WD_ALIGN_PARAGRAPH.CENTER
    for run in heading.runs:
        set_run_font(run, size=16, bold=True)

    paragraph = doc.add_paragraph()
    paragraph.paragraph_format.first_line_indent = Cm(0)
    paragraph.paragraph_format.space_before = Pt(6)
    add_field(paragraph, r'TOC \h \z \t "CaptionHinh,1"',
              "Danh mục hình ảnh tự động: bấm Update Field để cập nhật danh sách và số trang.", size=11)

    builder.note(
        "Ghi chú kỹ thuật: các danh mục trên là field của Word nên số trang chỉ hiển thị sau khi cập nhật. "
        "Thao tác: chọn toàn bộ văn bản (Ctrl+A) rồi nhấn F9, hoặc bấm chuột phải vào từng vùng và chọn "
        "Update Field → Update entire table."
    )


def appendix(doc, builder):
    builder.h1("PHỤ LỤC A: ĐỐI CHIẾU USE CASE VỚI MÃ NGUỒN")

    builder.p(
        "Phụ lục này nhằm chứng minh mỗi yêu cầu chức năng đã được hiện thực ở đâu trong mã nguồn, "
        "giúp người đọc kiểm tra chéo giữa báo cáo và sản phẩm bàn giao. Cột “Điểm vào” ghi rõ tệp và "
        "phương thức xử lý chính, cột “Dữ liệu tác động” liệt kê các bảng bị đọc hoặc ghi."
    )

    builder.table({
        "caption": "A.1: Đối chiếu Use Case với điểm vào trong mã nguồn",
        "cols": ["Use Case", "Điểm vào trong mã nguồn", "Dữ liệu tác động", "Cơ chế bảo vệ"],
        "widths": [3.0, 6.2, 3.6, 3.2],
        "font": 10.5,
        "rows": [
            ["UC01 – Quản lý biến thể",
             "QuanLyDesktop/FormBienTheSanPham.cs\nLoadVariants, btnThem_Click, btnSua_Click, VariantAlreadyExists",
             "ProductVariants (đọc, ghi)", "Kiểm tra trùng trong mã nguồn + UNIQUE index + CHECK tồn kho ≥ 0"],
            ["UC02 – Xử lý đơn hàng",
             "QuanLyDesktop/FormDonHang.cs\nLoadDonHang, btnDuyetDon_Click, UpdateOrderStatus, RestoreOrderStock",
             "Orders, OrderDetails, ProductVariants",
             "Danh sách trạng thái cho phép + UPDATE có điều kiện + SqlTransaction"],
            ["UC03 – Tìm kiếm và lọc sản phẩm",
             "WebBanHang/Controllers/ProductsController.cs\nHomeController.Index, ProductsController.Index",
             "Products, Categories, ProductVariants",
             "Tham số hóa truy vấn, escape ký tự LIKE, COLLATE Vietnamese_CI_AI"],
            ["UC04 – Đặt hàng trực tuyến",
             "WebBanHang/Controllers/CheckoutController.cs\nSqlOrderRepository.CreateOrderAsync",
             "Customers, Orders, OrderDetails, ProductVariants",
             "SqlTransaction, trừ kho có điều kiện, UNIQUE OrderCode + thử lại 3 lần"],
            ["UC05 – Tra cứu đơn hàng",
             "WebBanHang/Controllers/OrderLookupController.cs\nSqlOrderRepository.FindOrderForLookupAsync",
             "Orders, OrderDetails, Products",
             "Bắt buộc khớp đồng thời mã đơn và số điện thoại"],
            ["Đăng nhập quản trị",
             "QuanLyDesktop/Form1.cs – btnDangNhap_Click",
             "Users, Roles", "Tham số hóa truy vấn, kiểm tra IsActive, phân quyền theo RoleName"],
        ],
    })

    builder.h1("PHỤ LỤC B: DANH MỤC SƠ ĐỒ MERMAID VÀ HƯỚNG DẪN RENDER")

    builder.p(
        "Toàn bộ sơ đồ trong báo cáo được lưu dưới dạng mã nguồn Mermaid trong thư mục diagrams/ của "
        "kho mã nguồn. Cách làm này cho phép chỉnh sửa sơ đồ bằng văn bản, so sánh thay đổi bằng Git "
        "và xuất ảnh PNG độ phân giải cao khi cần in ấn. Bảng dưới đây liệt kê từng tệp và vị trí sơ đồ "
        "tương ứng trong báo cáo."
    )

    builder.table({
        "caption": "B.1: Danh mục tệp Mermaid và vị trí sử dụng trong báo cáo",
        "cols": ["Tệp trong diagrams/", "Loại sơ đồ", "Sử dụng tại"],
        "widths": [6.4, 4.4, 5.2],
        "font": 11,
        "rows": [
            ["01_system_architecture.mmd", "Kiến trúc tổng thể hệ thống", "Hình 1.1"],
            ["02_scrum_process.mmd", "Quy trình Scrum", "Hình 1.2"],
            ["03_mvc_request_flow.mmd", "Sequence Diagram – vòng đời request MVC", "Hình 2.1"],
            ["04_winforms_layers.mmd", "Kiến trúc phân tầng WinForms", "Hình 2.2"],
            ["05_activity_nhap_kho_bienthe.mmd", "Activity Diagram – nhập kho biến thể", "Hình 3.1"],
            ["06_activity_dat_hang_online.mmd", "Activity Diagram – đặt hàng trực tuyến", "Hình 3.2"],
            ["07_activity_xuly_don_hang.mmd", "Activity Diagram – xử lý đơn hàng", "Hình 3.3"],
            ["08_usecase_tongquan.mmd", "Use Case tổng quan", "Hình 3.4"],
            ["09_usecase_winforms.mmd", "Use Case phân hệ Desktop", "Hình 3.5"],
            ["10_usecase_web.mmd", "Use Case phân hệ Web", "Hình 3.6"],
            ["11_erd.mmd", "ERD mức logic", "Hình 3.7"],
            ["12_screenflow_web.mmd", "Screen Flow website", "Hình 3.8"],
            ["13_screenflow_winforms.mmd", "Screen Flow ứng dụng Desktop", "Hình 3.9"],
            ["14_deployment.mmd", "Sơ đồ triển khai", "Hình 3.10"],
        ],
    })

    builder.p("Cách render sơ đồ thành ảnh PNG chất lượng cao bằng Mermaid CLI:", indent=False)
    builder.code(
        "# Cài đặt một lần (cần Node.js 18 trở lên)\n"
        "npm install -g @mermaid-js/mermaid-cli\n"
        "\n"
        "# Render toàn bộ sơ đồ với bề rộng 2400 px, nền trắng\n"
        "mkdir diagrams\\png\n"
        "Get-ChildItem diagrams\\*.mmd | ForEach-Object {\n"
        "    $out = \"diagrams\\png\\\" + $_.BaseName + \".png\"\n"
        "    mmdc -i $_.FullName -o $out -w 2400 -b white\n"
        "}\n"
    )
    builder.p(
        "Sau khi có ảnh, chèn ảnh vào đúng khung tương ứng trong báo cáo (thay dòng chữ “Khung chèn sơ đồ”) "
        "và giữ nguyên phần chú thích bên dưới để danh mục hình ảnh vẫn cập nhật đúng."
    )


# ----------------------------------------------------------------------------
# main
# ----------------------------------------------------------------------------
def main():
    parser = argparse.ArgumentParser(description="Sinh báo cáo đồ án dạng .docx")
    parser.add_argument("--out", default=DEFAULT_OUT, help="Đường dẫn tệp .docx đầu ra")
    args = parser.parse_args()

    doc = setup_document()
    builder = ReportBuilder(doc)

    doc.core_properties.title = "Báo cáo đồ án cuối kỳ - Hệ thống quản lý và bán hàng thời trang"
    doc.core_properties.subject = "Ngôn ngữ lập trình C# - WinForms và ASP.NET Core MVC"
    doc.core_properties.author = "Nhóm sinh viên thực hiện"
    doc.core_properties.keywords = "WinForms, ASP.NET Core MVC, SQL Server, ADO.NET, thời trang"

    cover_page(doc)
    front_matter(doc, builder)

    builder.render(content_chuong1.BLOCKS)
    builder.render(content_chuong2.BLOCKS)
    builder.render(content_chuong3.BLOCKS)

    appendix(doc, builder)

    output = os.path.abspath(args.out)
    doc.save(output)

    print(f"Đã tạo báo cáo: {output}")
    print(f"Số hình: {builder.figure_count}")
    print(f"Kích thước: {os.path.getsize(output) / 1024:.1f} KB")


if __name__ == "__main__":
    sys.exit(main())
